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
			Console.WriteLine(market.SendQueryUserRequest());
			//Console.WriteLine(market.SendCancelBuySellRequest(1));
			//Console.WriteLine(market.SendBuyRequest(20, 2, 3));
			//Console.WriteLine(market.SendSellRequest(19, 2, 5));
			Console.WriteLine(market.SendQueryBuySellRequest(3));
			Console.WriteLine(market.SendQueryUserRequest());
			Console.ReadLine();
        }
    }
}
