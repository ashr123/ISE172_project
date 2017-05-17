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

namespace LogicTier

{
    public class Program
    {
        //Note: tell Saar turn on both timers while gui is running
        private static System.Timers.Timer amaAutoTimer;
        private static System.Timers.Timer userAutoTimer;
        private static int counter = 0;                           //counts server calls
        private static bool FLAG_isRunning = false;               //prevent creating lots of AMA running parallel
        private static List<UserAsksLink> userCommands;           //holds the user wanted actions


        public static void TurnOffBothTimers()
        {
            amaAutoTimer.Stop();
            userAutoTimer.Stop();
        }

        public static void TimerOfAMA(bool b)
        {

            if (amaAutoTimer == null)     //creates only one instance
            {
                amaAutoTimer = new System.Timers.Timer(2000);
                amaAutoTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                amaAutoTimer.AutoReset = true;
            }

            if (b)
            {
                userAutoTimer.Stop();            //not possible AMA auto & user requests
                amaAutoTimer.Start();
            }

            else
                amaAutoTimer.Stop();

        }


        private static void OnTimedEvent(object sender, EventArgs e)
        {
            if (!FLAG_isRunning)                     //for not creating lot of AMA functions running in parallel
            {
                Random rnd = new Random();
                int buyORsell = rnd.Next(0, 2);
                int num = rnd.Next(0, 10);

                //choose randomly to buy or to sell. and choose randomly commodity


                if (buyORsell == 1)      //ama buy
                    AMA_Buy(num, 15, 4);

                else                     //ama sell
                    AMA_Sell(num, 23, -1);                             //-1 means all


            }

        }

        public static void TimerOfUserAsks(List<UserAsksLink> userListCommands)
        {
            TimerOfAMA(false);

            userCommands = userListCommands;         //refreshing the user commands 

            //Note: tell saar to always keep old list as a field

            if (userAutoTimer == null)         //one instance
            {
                userAutoTimer = new System.Timers.Timer(4000);
                userAutoTimer.Elapsed += new ElapsedEventHandler(OnUSEREvent);
                userAutoTimer.AutoReset = true;
            }

            userAutoTimer.Start();

        }


        private static void OnUSEREvent(object sender, EventArgs e)
        {
            if (!FLAG_isRunning)                     //for not creating lot of AMA functions running in parallel
            {
                foreach (UserAsksLink ask in userCommands)
                {
                    if (ask.buyORsell == true)   //user wants to buy
                        AMA_Buy(ask.commodity, ask.desiredPrice, ask.amount);


                    else             //user wants to sell
                        AMA_Sell(ask.commodity, ask.desiredPrice, ask.amount);


                    Thread.Sleep(500);      //so all commands will run in the list without prevent each other

                }

            }// IF isRunning

        }


        public static void AMA_Buy(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;
            NotOverLoadServer();
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();
            counter++;


            foreach (ItemAskBid item in all.MarketInfo)
                if (item.Id == commodity && item.Info.Ask <= desiredPrice)
                {   //if item is the right commodity & right price

                    MarketUserData userData = client.SendQueryUserRequest();
                    counter++;
                    
                        List<int> l = userData.Requests;

                        if (l.Count != 0) {                //there are open requests in server

                        //if USER dont have enough money, we'll cancel his open buy requests- hoping after that he'll have enough
                        for (int i = l.Count; i >= 0 & userData.Funds < (item.Info.Ask * amount); i--)   //going from end so in delete won't change index of l
                            {
                                NotOverLoadServer();
                                
                                int reqID = l[i];    //saving the ID just for simplicity

                                MarketItemQuery request = client.SendQueryBuySellRequest(l[i]);
                                counter++;
                            if (request.Type.Equals("buy"))    //Note: check with roey
                            {

                                client.SendCancelBuySellRequest(reqID);
								HistoryLogger.WriteHistory(reqID, "Cancel", request.Commodity, request.Price, request.Amount);
								counter++;

                            }

                        }
                }

            if (userData.Funds >= item.Info.Ask * amount)
            {
                int ID = client.SendBuyRequest(item.Info.Ask + 1, commodity, amount).Id;
                //HistoryLogger.WriteHistory("Buy," + commodity + "," + (item.Info.Ask + 1) + "," + amount + "," + ID);
						HistoryLogger.WriteHistory(ID, "Buy", commodity, item.Info.Ask+1, amount);
						counter++;
            }

        }//bigIf

        FLAG_isRunning = false;
            return;
        }//AMAbuy




        public static void AMA_Sell(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;
            NotOverLoadServer();

            
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();
            counter++;

            MarketUserData userData = client.SendQueryUserRequest();
            counter++;

            foreach (int cmdty in userData.Commodities.Keys) {    //check if we own that commodity
                if (cmdty == commodity & cmdty.Tkey > 0)
                {
                    //passing on commodities list, until arriving the wished one
                    foreach (ItemAskBid item in all.MarketInfo)
                        if (item.Id == commodity && item.Info.Bid >= desiredPrice)
                        {                        //if item is the right commodity & right price

                            if (amount > cmdty.Tkey | amount ==-1)                //we cant sell more than we have OR -1 is our sign to sell ALL
                                amount = cmdty.Tkey;

                            //Note: ask roey about error
                            int ID = client.SendSellRequest(item.Info.Bid - 1, commodity, amount).Id;
                            //HistoryLogger.WriteHistory("Sell," + commodity + "," + (item.Info.Bid - 1) + "," + amount + "," + ID);
							HistoryLogger.WriteHistory(ID, "Sell", commodity, item.Info.Bid-1, amount);


							counter++;
                        }
                }
            }

            FLAG_isRunning = false;
            return;
        }//AMAsell


        private static void NotOverLoadServer()    //have to waste time, not overload the server
        {
            if (counter >= 16)
            {
                Thread.Sleep(10000);
                counter = 0;
            }

        }



    }
    }












