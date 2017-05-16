using log4net;
using DataTier.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;

namespace DataTier
{
    class Class1
    {
		public static ILog myLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		static void Main(string[] args)
        {
			Timer a = new Timer(3000);
			IMarketClient market = new MarketClientClass();
			a.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
			Console.WriteLine(market.SendCancelBuySellRequest(5772569));
			Console.WriteLine(market.SendQueryUserRequest());
			Console.WriteLine(market.QueryUserRequests());
			Console.WriteLine(market.QueryAllMarketRequest());
			
			//Trace.WriteLine(market.QueryAllMarketRequest());
			//HistoryLogger.WriteHistory(market.SendQueryUserRequest().ToString());
			//myLogger.Debug("asdasdasdasdasd");
			Console.ReadLine();
        }
		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("Hello World!");
		}
	}
}
