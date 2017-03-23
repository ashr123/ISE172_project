using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class QueryMarketRequest
    {
        int Commodity { get; set; }
        String Type { get; set; }
        public QueryMarketRequest(int commodity)
        {
            this.Commodity = commodity;
        }
    }
}
