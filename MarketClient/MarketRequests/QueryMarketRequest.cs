using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.MarketRequests
{
    //Query about the market request
    public class QueryMarketRequest
    {
		public int commodity;
        public readonly string type;
        public QueryMarketRequest(int commodity)
        {
			type="queryMarket";
			this.commodity=commodity;
		}
    }
}
