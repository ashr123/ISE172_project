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
        const string type="queryMarket";
        public QueryMarketRequest(int commodity)
        {
            this.Commodity=commodity;
        }
    }
}
