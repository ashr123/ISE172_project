using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using log4net;
using System.Collections.ObjectModel;

namespace DataTier.Loggers
{
	public class HistoryLogger
	{
		//static ILog myLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly ILog myLogger = LogManager.GetLogger("History");
		public static void WriteHistory(int requestId, string action, int commodity, int price, int amount)
		{
			//File.AppendAllText(@"..\..\..\Log\history.txt", DateTime.Now+","+contents+"\n");
			myLogger.Info(requestId+","+action+','+commodity+','+price+','+amount);
		}

		public static ObservableCollection<Record> ReadHistory()
		{
			//List<int> UserActiveRequests=new MarketClientClass().SendQueryUserRequest().Requests;
			string[] history = File.ReadAllLines(@"..\..\..\Log\history.txt");
			ObservableCollection<Record> output = new ObservableCollection<Record>();
			foreach (string line in history)
			{
				string[] temp = line.Split(',');
				output.Add(new Record()
				{
					Time=Convert.ToDateTime(temp[0]).ToString(),
					RequestId=Int32.Parse(temp[1]),
					//IsExecuted=!UserActiveRequests.Contains(Int32.Parse(temp[1])),
					Action=temp[2],
					Commodity=Int32.Parse(temp[3]),
					Price=Int32.Parse(temp[4]),
					Amount=Int32.Parse(temp[5])
				});
			}
			return output;
		}
	}

	public class Record
	{
		public bool IsExecuted { get; set; }
		public string Time { get; set; }
		public int RequestId { get; set; }
		public string Action { get; set; }
		public int Commodity { get; set; }
		public int Price { get; set; }
		public int Amount { get; set; }
	}
}