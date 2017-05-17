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
        private static System.Timers.Timer myTimer;
        private static int counter = 0;
        private static bool FLAG_isRunning = false;


        public static void TimerOfAMA(bool b)
        {


            if (myTimer == null)
            {
                myTimer = new System.Timers.Timer(2000);
                myTimer.Elapsed += new ElapsedEventHandler(onTimedEvent);
                myTimer.AutoReset = true;
            }

            if (b)
                myTimer.Start();


            else
                myTimer.Stop();

        }


        private static void onTimedEvent(object sender, EventArgs e)
        {
            if (!FLAG_isRunning)                     //for not creating lot of AMA functions running in parallel
            {
                Random rnd = new Random();
                int num = rnd.Next(0, 10);
                int buyORsell = rnd.Next(0, 2);

                if (buyORsell == 1) //ama buy
                    AMA_Buy(num, 15, 4);

                else                    //ama sell
                    AMA_Sell(num, 23, -1);                             //-1 means all


            }


        }




        public static void AMA_Buy(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();
            counter++;


            foreach (ItemAskBid item in all.MarketInfo)
                if (item.Id == commodity && item.Info.Ask <= desiredPrice)
                {   //if item is the right commodity & right price

                    MarketUserData userData = client.SendQueryUserRequest();
                    counter++;

                    while (userData.Funds < (item.Info.Ask * amount))
                    {

                        if (counter >= 16)                   //have to waste time, not overload the server
                        {
                            Thread.Sleep(10000);
                            counter = 0;
                        }


                        List<int> l = userData.Requests;

                        // List<int> listOfUserRequests = ((MarketUserRequests) client.QueryUserRequests()).Requests;

                        if (l.Count == 0)          //there are no more open requests
                            break;

                        client.SendCancelBuySellRequest(l[0]);
                        HistoryLogger.WriteHistory(l[0] + "," + "Canceled");
                        counter++;
                        userData = client.SendQueryUserRequest();     //refresh data
                        counter++;

                    }

                    if (userData.Funds >= item.Info.Ask * amount)
                    {
                        int ID = client.SendBuyRequest(item.Info.Ask + 1, commodity, amount).Id;
                        HistoryLogger.WriteHistory("Buy," + commodity + "," + (item.Info.Ask + 1) + "," + amount + "," + ID);
                        counter++;
                    }

                }//bigIf

            FLAG_isRunning = false;
            return;
        }//AMAbuy



        public static void AMA_Sell(int commodity, int desiredPrice, int amount)
        {
            FLAG_isRunning = true;

            if (counter >= 16)                   //have to waste time, not overload the server
            {
                Thread.Sleep(10000);
                counter = 0;
            }

            
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();
            counter++;

            MarketUserData userData = client.SendQueryUserRequest();
            counter++;

            foreach (Dictionary<string, int> cmdty in userData.Commodities) {    //check if we own that commodity
                if (cmdty.Tsring == commodity & cmdty.Tkey > 0)
                {
                    //passing on commodities list, until arriving the wished one
                    foreach (ItemAskBid item in all.MarketInfo)
                        if (item.Id == commodity && item.Info.Ask >= desiredPrice)
                        {                        //if item is the right commodity & right price

                            if (amount > cmdty.Tkey | amount ==-1)                //we cant sell more than we have OR -1 is our sign to sell ALL
                                amount = cmdty.Tkey;


                            int ID = client.SendSellRequest(item.Info.Bid - 1, commodity, amount).Id;
                            HistoryLogger.WriteHistory("Sell," + commodity + "," + (item.Info.Bid - 1) + "," + amount + "," + ID);
                            counter++;
                        }
                }
            }

            FLAG_isRunning = false;
            return;
        }//AMAsell

    }
    }












