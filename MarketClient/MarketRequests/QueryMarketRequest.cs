using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class QueryMarketRequest
    {
		public int Commodity;
        public readonly string type;
        public QueryMarketRequest(int commodity)
        {
			type="queryMarket";
			Commodity=commodity;
		}
    }
}
