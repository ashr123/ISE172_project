using log4net;
using DataTier.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    class Class1
    {
		public static ILog myLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		static void Main(string[] args)
        {
			HistoryLogger.WriteHistory("fffffffffffffffff");
			//myLogger.Debug("asdasdasdasdasd");
			Console.ReadLine();
        }
    }
}
