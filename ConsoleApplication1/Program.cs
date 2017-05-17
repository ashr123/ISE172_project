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
                //ama buy
                Random rnd = new Random();
                int num = rnd.Next(0, 10);
                AMA_Buy(num, 10, 5);


                //ama sell
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
                        client.SendBuyRequest(item.Info.Ask + 1, commodity, amount);
                        HistoryLogger.WriteHistory(item.Info.Ask + "," + commodity + "," + amount + "," + "new Buy Request");
                        counter++;
                    }

                }//bigIf

            FLAG_isRunning = false;
            return;
        }//AMA
    }
}










