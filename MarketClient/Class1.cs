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
			HistoryLogger.WriteHistory(123456, "Buy", 1, 1, 2);
			HistoryLogger.WriteHistory(123456, "Cancel", 1, 1, 2);
			HistoryLogger.WriteHistory(123457, "Buy", 2, 1, 2);
			HistoryLogger.WriteHistory(123458, "Sell", 3, 1, 2);
			//Console.WriteLine(market.QueryUserRequests());
			Trace.WriteLine(market.QueryAllMarketRequest());
			//HistoryLogger.WriteHistory(market.SendQueryUserRequest().ToString());
			//myLogger.Debug("asdasdasdasdasd");
			Console.ReadLine();
        }
    }
}
