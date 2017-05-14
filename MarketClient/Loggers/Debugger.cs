using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Loggers
{
	public class Debugger
	{
		private static readonly ILog myLogger = LogManager.GetLogger("Debugger");

		public static void Debug(string message)
		{
			myLogger.Debug(message);
		}
	}
}
