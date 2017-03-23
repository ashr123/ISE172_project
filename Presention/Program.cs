using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presention
{
    class Program
    {//do you see?
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, This is an Algo-trading market system.\n We Support the following operations: ");
            Console.WriteLine("Press 1 to open a new request (buy/sell)\n Press 2 to cancel an exist request\n Press 3 to ask a query \n Press 4 to");
            String input = Console.ReadLine();

            if (input.Length!=1)
            {
                Console.WriteLine("You entered illegal input.");
                Console.WriteLine("Press 1 to open a new request (buy/sell)\n Press 2 to cancel an exist request\n Press 3 to ask a query \n Press 4 to");
            }//if

            else
            {
                
                    }
        }
    }
}
