using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.Market_Requests
{
    //Comment
    class QueryMarketRequest
    {
        int Commodity { get; set; }

        public QueryMarketRequest(int commodity)
        {
            this.Commodity = commodity;
        }
    }
}
