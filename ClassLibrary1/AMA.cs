using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using DataTier.DataEntries;
using System.Timers;
using DataTier.Loggers;
using System.Threading;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace LogicTier
{
    public class AMA
    {
        private static System.Timers.Timer amaAutoTimer;
        private static System.Timers.Timer userAutoTimer;
        private static int counterServerCalls = 0;                           //counts server calls
        private static bool FLAG_isRunning = false;             //prevent creating lots of AMA running parallel
        private static List<UserAsksLink> userCommands;           //holds the user wanted actions
        private static bool FLAG_buyOrSell = false;               //Alternately buy-sell
        

        public static void ResetBothTimers()           //will create only one instance of timers & stop them both- to prevent running parallel
        {
            if (userAutoTimer == null)         //one instance
            {
                userAutoTimer = new System.Timers.Timer(6000);
                userAutoTimer.Elapsed += new ElapsedEventHandler(OnUSEREvent);
                userAutoTimer.AutoReset = false;
            }
            userAutoTimer.Stop();

            if (amaAutoTimer == null)     //creates only one instance
            {
                amaAutoTimer = new System.Timers.Timer(6000);
                amaAutoTimer.Elapsed += new ElapsedEventHandler(OnAMAEvent);
                amaAutoTimer.AutoReset = true;
            }
            amaAutoTimer.Stop();
        }

        public static bool checkAmaAutoTimerIsSet()      //for test8
        {
            if (amaAutoTimer == null)
                return false;

            else
                return true;
        }
        
        public static bool checkUserAmaTimerIsSet()       //for test8
        {

            if (userAutoTimer == null)
                return false;

            else
                return true;
        }




        public static void TimerOfAMA(bool b)
        {
            ResetBothTimers();              //not possible AMA auto & user requests

            if (b)
                amaAutoTimer.Start();
        }


        private static void OnAMAEvent(object sender, EventArgs e)
        {
            if (!FLAG_isRunning)                     //for not creating lot of AMA functions running in parallel
            {

                Trace.WriteLine("AAAAAAMMMMMMAAAAAA");
                
                using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
                {
                    Random rnd = new Random();
                    int avgPrice=0;
                    int rndCommodity = rnd.Next(1, 10);
                    int amountToBuy = rnd.Next(4, 10);
                    int amountToSell = rnd.Next(-1, 8);           //-1 means all


                    if (amountToSell == 0)
                        amountToSell = -1;

                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select Avg(price) AS AveragePrice from items where commodity='" + rndCommodity + "' and timestamp>= DATEADD(mi, -30, GETUTCDATE())", myConnection);
                    SqlDataReader myDataReader = myCommand.ExecuteReader();
                    if (myDataReader.HasRows)

                    {
                        myDataReader.Read();
                        NotOverLoadServer();
                        try
                        {
                            Trace.WriteLine(myDataReader[0].ToString());
                            Trace.WriteLine(Double.Parse(myDataReader[0].ToString()));
                            Double avgPriceDouble = Double.Parse(myDataReader[0].ToString());
                            avgPrice = Convert.ToInt32(avgPriceDouble);
                        }
                        catch
                        {
                            return;
                        }
                        //want to get INFO for the commodity ASK-BID
                        MarketClientClass client = new MarketClientClass();
                        MarketCommodityOffer commodityInfo = client.SendQueryMarketRequest(rndCommodity);
                        NotOverLoadServer();

                        if (commodityInfo.Error != null)
                            return;

                        //choose alternately buy or sell. and choose randomly commodity
                        if (FLAG_buyOrSell)
                        {    //ama buy
                            if (avgPrice - 15 < 0)
                            {
                                avgPrice = rnd.Next(4, 9);
                                AMA_Buy(rndCommodity, avgPrice, amountToBuy);
                            }
                            else
                            {
                                avgPrice -= 15;
                                AMA_Buy(rndCommodity, avgPrice, amountToBuy);
                            }
                        }
                        else
                        {     //ama sell  
                            avgPrice += 4;
                            AMA_Sell(rndCommodity, avgPrice , amountToSell);
                        }
                        FLAG_buyOrSell = !FLAG_buyOrSell;
                    }
                }
            }
        }

        public static void TimerOfAutoUser(List<UserAsksLink> userListCommands)
        {
            ResetBothTimers();

            userCommands = userListCommands;         //refreshing the user commands field

            //Note: tell saar to always keep old list as a field
            userAutoTimer.Start();
        }


        private static void OnUSEREvent(object sender, EventArgs e)
        {
			Trace.WriteLine("UUUSSSEEERRR AAAMMMAAA");
            if (!FLAG_isRunning)                     //for not creating lot of AMA functions running in parallel
            {
                foreach (UserAsksLink ask in userCommands)
                {
                    if (!(ask.Commodity < 0 | ask.Commodity > 9 | ask.Amount == 0 ))             //check valid input
                    {

                        if (ask.Amount > 100)             //insure valid input
                            ask.Amount = 100;


                        if (ask.BuyORsell == true)   //user wants to buy
                            AMA_Buy(ask.Commodity, ask.DesiredPrice, ask.Amount);


                        else             //user wants to sell
                            AMA_Sell(ask.Commodity, ask.DesiredPrice, ask.Amount);

                        Thread.Sleep(6000);      //so all commands will run in the list without prevent each other
                    }
                }
            }// IF isRunning
        }

        public static void AMA_Buy(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;
            NotOverLoadServer();

            MarketClientClass client = new MarketClientClass();
            MarketUserData userData = client.SendQueryUserRequest();
            NotOverLoadServer();

            if (userData.Error != null)
            {
                FLAG_isRunning = false;
                return;
            }

            if (userData.Funds >= desiredPrice * amount)    //if we have enough money- just buy and finish running.
            {
                MarketBuySell buyreq = client.SendBuyRequest(desiredPrice, commodity, amount);
                NotOverLoadServer();

                if (buyreq.Error == null)          //the buy req is successfuly passed to the server
                    HistoryLogger.WriteHistory(buyreq.Id, "Buy", commodity, desiredPrice, amount);
                FLAG_isRunning = false;
                return;
            }

            //if USER dont have enough money, we'll cancel his open buy requests- hoping after that he'll have enough
            List<int> l = userData.Requests;

            if (l.Count == 0)               //there are NO open requests in server
            {
                FLAG_isRunning = false;
                return;
            }

            for (int i = l.Count - 1; i >= 0 && userData.Funds < (desiredPrice * amount); i--)   //going from end so in delete won't change index of l
            {
                int reqID = l[i];    //saving the ID just for simplicity

                MarketItemQuery request = client.SendQueryBuySellRequest(reqID);
                NotOverLoadServer();

                if (request.Error != null)
                {
                    FLAG_isRunning = false;
                    return;
                }

                //wish to cancel only buy requests. only this kind of canceling request give back money
                //func SendCancelBuySellRequest returns bool - of the action passed successfuly
                if (request.Type.Equals("buy") && client.SendCancelBuySellRequest(reqID))
                    HistoryLogger.WriteHistory(reqID, "Cancel", request.Commodity, request.Price, request.Amount);

                NotOverLoadServer();
            }

            userData = client.SendQueryUserRequest();   //refresh data
            NotOverLoadServer();

            if (userData.Error != null)
            {
                FLAG_isRunning = false;
                return;
            }

            if (userData.Funds >= desiredPrice * amount)    //if NOW we have enough money-  buy 
            {
                MarketBuySell buyreq = client.SendBuyRequest(desiredPrice, commodity, amount);
                NotOverLoadServer();

                if (buyreq.Error == null)          //the buy req is successfuly passed to the server
                    HistoryLogger.WriteHistory(buyreq.Id, "Buy", commodity, desiredPrice, amount);
            }
            FLAG_isRunning = false;
            return;
        }//AMAbuy

        public static void AMA_Sell(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;
            NotOverLoadServer();

            MarketClientClass client = new MarketClientClass();
            MarketUserData userData = client.SendQueryUserRequest();
            NotOverLoadServer();

            if (userData.Error != null)
            {
                FLAG_isRunning = false;
                return;
            }

            foreach (int cmdty in userData.Commodities.Keys)    //passing on all commodities
            {
                if (cmdty == commodity && userData.Commodities[cmdty] > 0)           //check if we own that commodity
                {
                    //if item is the right commodity & we own it
                    if (amount > userData.Commodities[cmdty] || amount == -1)                //we cant sell more than we have OR -1 is our sign to sell ALL
                        amount = userData.Commodities[cmdty];

                    MarketBuySell sellreq = client.SendSellRequest(desiredPrice, commodity, amount);
                    NotOverLoadServer();

                    if (sellreq.Error == null)        //the sell req is successfuly passed to the server
                        HistoryLogger.WriteHistory(sellreq.Id, "Sell", commodity, desiredPrice, amount);
                }
            }
            FLAG_isRunning = false;
            return;
        }//AMAsell

        private static void NotOverLoadServer()    //have to waste time, not overload the server
        {
            counterServerCalls++;
            if (counterServerCalls >= 16)
            {
                Thread.Sleep(10000);
                counterServerCalls = 0;
            }
        }




    }//class
}//namespace