using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.MarketRequests
{
	public class QueryAllMarketRequest
	{
		public readonly string type;
		public QueryAllMarketRequest()
		{
			type="queryAllMarket";
		}
	}
}
