using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.DataEntries
{
	public class AllMarketRequest
	{
		public Exception Error { get; set; }
		public List<ItemAskBid> MarketInfo { get; set; }

		public override string ToString()
		{
			if (Error!=null)
				return Error.Message;
			string output = "[";
			foreach (ItemAskBid i in MarketInfo)
			{
				output+=i;
				if (i!=MarketInfo[MarketInfo.Count-1])
					output+=", ";
			}
			return output+="]";
		}
	}
}
