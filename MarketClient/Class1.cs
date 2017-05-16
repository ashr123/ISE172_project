using log4net;
using DataTier.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataTier
{
    class Class1
    {
		//public static ILog myLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		static void Main(string[] args)
        {
			IMarketClient market = new MarketClientClass();

			Console.WriteLine(market.QueryUserRequests());
			Console.WriteLine(market.QueryAllMarketRequest());
			//Console.WriteLine(market.SendBuyRequest(1, 7, 1));
			//HistoryLogger.WriteHistory(market.SendQueryUserRequest().ToString());
			//myLogger.Debug("asdasdasdasdasd");
			Console.ReadLine();
        }
    }
}
