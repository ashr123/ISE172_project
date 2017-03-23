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
            IMarketClient market = new MarketClientClass();
            Console.Write(market.SendQueryMarketRequest(0));
        }
    }
}
