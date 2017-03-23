using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presention
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, This is an Algo-trading market system.\n We Support the following operations: ");
            Console.WriteLine("Press 1 to open a new request (buy/sell)\n Press 2 to cancel an exist request\n Press 3 to ask a query \n");
            String input = Console.ReadLine();

            bool isLegal = checkLegality(input, 3);
            while (isLegal == false)
                PrintWhenIllegalkey();


            if (input.Equals('1'))
            {
                Console.WriteLine("You chose to open new request./n To buy press 1/n To sell press 2");
                input = Console.ReadLine();

                isLegal = checkLegality(input, 2);
                while (isLegal == false)
                    PrintWhenIllegalkey();

                CollectingInfoBUYSELL(input);
            }

            if (input.Equals('2'))
            {
                Console.WriteLine("You chose to cancel a request./n");
                


            }


                if (input.Equals('3'))
                    Console.WriteLine("You chose to ask a query./n To buyQuery press 1/n To sellQuery press 2/n To userQuery press 3/n To marketQuery press 2 ");

         
        }

        private static bool checkLegality(String s, int options) {

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

        private static void CollectingInfoBUYSELL(int a)
        {
            Console.WriteLine("Please enter Commodity");
            int commodity = Console.ReadKey();
            Console.WriteLine("Please enter Amount");
            int amount = Console.ReadKey();
            Console.WriteLine("Please enter Price");
            int price = Console.ReadKey();

            if (a.Equals('1')) {
                BuyRequest buy = new BuyRequest;
                buy.Commodity = commodity;
                buy.Amount = amount;
                buy.Price = price;
            }
            else
            {
                SellRequest sell = new SellRequest;
                sell.Commodity = commodity;
                sell.Amount = amount;
                sell.Price = price;
            }

            //צריכה לקבל אישור קנייה ולהדפיס
        }

        private static void CollectInfoCancelRequst()
        {
            Console.WriteLine("Please enter the ID request you wish to cancel./n");
            int id=Console.ReadKey();

            CancelBuySellRequest cancel = new CancelBuySellRequest;
            cancel.Id = id;

        }
    }
}
