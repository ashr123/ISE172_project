using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient
{
    class Class1
    {
        static void Main(string[] args)
        {
			//Console.Write(Convert.ToInt32("f"));
			IMarketClient market=new MarketClientClass();
			Console.Write(market.SendQueryUserRequest());
			Console.ReadLine();
        }
    }
}
