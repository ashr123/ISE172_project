using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class QueryMarketRequest
    {
        int Commodity { get; set; }
        public string type;
        public QueryMarketRequest(int commodity)
        {
			type="queryMarket";
			Commodity=commodity;
		}
    }
}
