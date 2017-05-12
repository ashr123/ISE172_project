using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net;

namespace DataTier.Loggers
{
	public class HistoryLogger
	{
		//static ILog myLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly ILog myLogger = LogManager.GetLogger("History");
		public static void WriteHistory(string contents)
		{
			//File.AppendAllText(@"..\..\..\Log\history.txt", DateTime.Now+","+contents+"\n");
			myLogger.Info(contents);
		}

		public static string[][] ReadHistory()
		{
			string[] history=File.ReadAllLines(@"..\..\..\Log\history.txt");
			string[][] output=new string[history.Length][];
			for (int i = 0; i<history.Length; i++)
			{
				output[i]=history[i].Split(',');
			}
			return output;
		}
	}
}