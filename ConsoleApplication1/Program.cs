using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using DataTier.DataEntries;


namespace LogicTier

{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();

            myTimer.Interval = 2000;
            myTimer.Start();

            myTimer.Stop();


            myTimer.Tick += new EventHandler(TimerEventProcessor);

            myTimer.
            private static void myTimer_Tick(object sender, EventArgs e) {

            }
        }

        public static void AMA (int commodity, int desiredPrice, int amount ){
            MarketClientClass client = new MarketClientClass();
            AllMarketRequest all = client.QueryAllMarketRequest();


        foreach (ItemAskBid item in all.MarketInfo)
            if (item.Id == commodity && item.Info.Ask <= desiredPrice) {   //if item is the right commodity & right price
                
                    MarketUserData userData = client.SendQueryUserRequest();
                    while (userData.Funds < (item.Info.Ask * amount))
                    {
                        List<int> l = userData.Requests;

                                      //maybe use new methood?
                        // List<int> listOfUserRequests = ((MarketUserRequests) client.QueryUserRequests()).Requests;

                        if (l == null)          //notSure. what if theres no more requests?
                            break;

                        client.SendCancelBuySellRequest(l[0]);
                        userData = client.SendQueryUserRequest();     //refresh data
                    }

                    if (userData.Funds >= item.Info.Ask * amount)
                        client.SendBuyRequest(item.Info.Ask, commodity, amount);

                }//bigIf

          }//AMA
            
    }
    

    public timer()
    {

        TIMER(2000){
            public ama;
        }
//לעשות כפתור להפסקת טיימר
//
    }
}
