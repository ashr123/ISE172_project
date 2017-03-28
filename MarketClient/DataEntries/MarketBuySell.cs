using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.DataEntries
{
	public class MarketBuySell
	{
		public string Error { get; set; }
		public int Id { get; set; }
		public override string ToString()
		{
			if (Error!=null)
				return Error;
			return Convert.ToString(Id);
		}
	}
}
