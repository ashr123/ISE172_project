using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketClient;

namespace Presention
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, This is an Algo-trading market system.\n We Support the following operations: ");
            Console.WriteLine("Press 1 to open a new request (buy/sell)\n Press 2 to cancel an exist request\n Press 3 to ask a query \n");
            String input = Console.ReadLine();

            bool isLegal = CheckLegality(input, 3);
            while (isLegal == false)
                PrintWhenIllegalkey();
            switch (input)
            {
                case "1":
                    Console.WriteLine("You chose to open a new request./n To buy press 1/n To sell press 2");
                    string input1 = Console.ReadLine();
                    CollectingInfoBUYSELL(input1);
                    break;

                case "2":
                    Console.WriteLine("You chose to cancel a request./n");
                    CollectInfoCancelRequst();
                    break;

                case "3":
                    Console.WriteLine("You chose to ask a query./n To buy/sellQuery press 1/n To userQuery press 2/n To marketQuery press 3 ");
                    string input3 = Console.ReadLine();
                    CollectInfoQueryRequst(input);
                    break;

                default:
                    
                    break;
            }//switch

        }//main


        private static void IsLegalCombinedLoop(String s, int options)
        {
            bool isLegal = CheckLegality(s, options);
            while (isLegal == false)
            {
                PrintWhenIllegalkey();
                s = Console.ReadLine();
                isLegal = CheckLegality(s, options);
            }
            return;
        }
        private static bool CheckLegality(String s, int options)
        {

            if (s.Length != 1)
                return false;
            for (int i = 1; i <= options; i++)
                if (s.Equals('i'))
                    return true;

            return false;
        }

        private static void PrintWhenIllegalkey()
        {
            Console.WriteLine("You entered illegal input. Please press a legal key.");
            return;
        }

        private static void CollectingInfoBUYSELL(string a)
        {
            //write more legallity checks
            Console.WriteLine("Please enter Commodity");
            int Commodity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter Amount");
            int Amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter Price");
            int Price = Convert.ToInt32(Console.ReadLine());

            MarketClientClass client = new MarketClientClass(); //create client to use it's methoods

            //write more legallity checks
            int IDbuysell;
            if (a.Equals('1'))
            {
                IDbuysell = client.SendBuyRequest(Price, Commodity, Amount);
            }
            else
            {
                IDbuysell = client.SendSellRequest(Price, Commodity, Amount);
            }

            Console.WriteLine("The request done successfully. The ID is " + IDbuysell);
            //print Id if well


            //add what if not succeed?
            return;
        }//collect info buy-sell request

        private static void CollectInfoCancelRequst()
        {
            Console.WriteLine("Please enter the ID request you wish to cancel./n");
            int id = Console.ReadKey();


            MarketClientClass client = new MarketClientClass(); //create client to use it's methoods

            bool ans = client.SendCancelBuySellRequest(id);

            if (ans == true)
                Console.WriteLine("Cancellation succeed./n");
            else
                Console.WriteLine("Cancellation failed./n");
            //print answer

            return;
        }//collect info cancel request

        private static void CollectInfoQueryRequst(string a)
        {

            MarketClientClass client = new MarketClientClass(); //create client to use it's methoods


            switch (a)
            {
                case "1":
                    Console.WriteLine("Please enter the ID request./n");
                    int id = Convert.ToInt32(Console.ReadLine());

                    MarketItemQuery data1 = client.SendQueryBuySellRequest(id);
                    Console.WriteLine("Buy-Sell query info is:/n" + data1.ToString());  //print data on a certain deal

                    break;

                case "2":
                    MarketUserData data2 = client.SendQueryUserRequest;

                    Console.WriteLine("User query info is:/n" + data2.ToString());
                    break;

                case "3":
                    Console.WriteLine("Please enter the Commodity./n");
                    int commodity = Convert.ToInt32(Console.ReadLine());

                    MarketCommodityOffer data3 = client.SendQueryMarketRequest(commodity);
                    Console.WriteLine("Market query info is:/n" + data3.ToString());
                    break;

            }//switch
            return;
        }//collect info query

    }//class 
}//namespace presention
