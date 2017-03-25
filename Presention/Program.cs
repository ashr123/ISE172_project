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
            String input=Console.ReadLine();

            bool isLegal=CheckLegality(input, 3);
            while (isLegal == false)
                PrintWhenIllegalkey();


            if (input.Equals('1'))
            {
                Console.WriteLine("You chose to open a new request./n To buy press 1/n To sell press 2");
                input=Console.ReadLine();

                isLegal=CheckLegality(input, 2);
                while (isLegal == false)
                {
                    PrintWhenIllegalkey();
                    input = Console.ReadLine();
                    isLegal = CheckLegality(input, 2);
                }

                CollectingInfoBUYSELL(input);
            }

            if (input.Equals('2'))
            {
                Console.WriteLine("You chose to cancel a request./n");

                CollectInfoCancelRequst();
            }


            if (input.Equals('3'))
            {
                Console.WriteLine("You chose to ask a query./n To buy/sellQuery press 1/n To userQuery press 2/n To marketQuery press 3 ");
                input=Console.ReadLine();

                isLegal=CheckLegality(input, 3);
                while (isLegal == false)
                    PrintWhenIllegalkey();

                CollectInfoQueryRequst(input);
            }

        }

        private static bool CheckLegality(String s, int options) {

            if (s.Length != 1)
                return false;
            for(int i=1; i <= options; i++)
                if (s.Equals('i'))
                    return true;

            return false;
        }
        
        private static void PrintWhenIllegalkey()
        {
            Console.WriteLine("You entered illegal input. Please press a llegal key.");
        }

        private static void CollectingInfoBUYSELL(string a)
        {
            Console.WriteLine("Please enter Commodity");
            int commodity=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter Amount");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter Price");
            int price = Convert.ToInt32(Console.ReadLine());


            //write more legallity checks

            if (a.Equals('1')) {
                BuyRequest buy=new BuyRequest;
                buy.Commodity=commodity;
                buy.Amount=amount;
                buy.Price=price;
            }
            else
            {
                SellRequest sell=new SellRequest;
                sell.Commodity=commodity;
                sell.Amount=amount;
                sell.Price=price;
            }

            //print if well
        }

        private static void CollectInfoCancelRequst()
        {
            Console.WriteLine("Please enter the ID request you wish to cancel./n");
            int id=Console.ReadKey();

            CancelBuySellRequest cancel=new CancelBuySellRequest;
            cancel.Id=id;
            //print if well

        }

        private static void CollectInfoQueryRequst(string a)
        {
            if (a.Equals('1'))
            {
                Console.WriteLine("Please enter the ID request./n");
                int id=Console.ReadKey();

                QueryBuySellRequest query=new QueryBuySellRequest;
                query.Id=id;

                //print on a certain deal
            }


            if (a.Equals('2'))
            {
                QueryUserRequest query=new QueryUserRequest;
                //print user inf
            }

            if (a.Equals('3'))
            {
                Console.WriteLine("Please enter the Commodity./n");
                int commodity=Console.ReadKey();

                QueryMarketRequest query=new QueryMarketRequest;
                query.Commodity=commodity;

                //print comm inf

            }

        }
    }
}
