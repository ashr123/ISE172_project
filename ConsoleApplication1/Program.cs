using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using DataTier.DataEntries;
using System.Timers;
using DataTier.Loggers;

namespace LogicTier

{
    class Program
    {
        
        public static void TimerOfAMA(bool b)
        {
            Timer myTimer = new Timer(2000);
            myTimer.Elapsed += new ElapsedEventHandler(onTimedEvent);

            if (b)
                myTimer.Start();
            

            else
                myTimer.Stop();

        }
        

            private static void onTimedEvent(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 10);
            AMA_Buy(num, 2, 8);


            //ama sell
        }

        private static void wasteTime(object sender, EventArgs e)
        {

                      //nothing
            return;

        }



        public static void AMA_Buy(int commodity, int desiredPrice, int amount)
        {
            int counter = 0;
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();
            counter++;


            foreach (ItemAskBid item in all.MarketInfo)
                if (item.Id == commodity && item.Info.Ask <= desiredPrice)
                {   //if item is the right commodity & right price

                    MarketUserData userData = client.SendQueryUserRequest();
                    counter++;

                    Timer timeWaster = new Timer(10000);
                    timeWaster.Elapsed += new ElapsedEventHandler(wasteTime);


                    while (userData.Funds < (item.Info.Ask * amount))
                    {

                        if (counter == 19)                   //have to waste time, not overload the server
                        {
                            timeWaster.Start();
                            timeWaster.Stop();   //??? 
                            counter = 0;
                        }
                         

                            List<int> l = userData.Requests;

                        // List<int> listOfUserRequests = ((MarketUserRequests) client.QueryUserRequests()).Requests;

                        if (l.Count == 0)          //there are no more open requests
                            break;

                        client.SendCancelBuySellRequest(l[0]);
                        HistoryLogger.WriteHistory( l[0] + "," + "Canceled");
                        counter++;
                        userData = client.SendQueryUserRequest();     //refresh data
                        counter++;

                    }

                    if (userData.Funds >= item.Info.Ask * amount)
                    {
                        client.SendBuyRequest(item.Info.Ask, commodity, amount);
                        HistoryLogger.WriteHistory(item.Info.Ask+   ","+ commodity    +","   + amount+  ","  + "Buy Request");
                        counter++;
                    }

                }//bigIf

            return;
        }//AMA
    }
}
   

    




    


