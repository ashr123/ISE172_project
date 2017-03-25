using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketClient
{
    class Class1
    {
		static IMarketClient market = new MarketClientClass();
		static void Main(string[] args)
        {
			//Console.Write(Convert.ToInt32("f"));
			Console.Write(market.SendQueryUserRequest());
			Console.ReadLine();
        }
    }
}
