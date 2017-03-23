using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class QueryBuySellRequest
    {
        int Id { get; set; }
        const string type = "queryBuySell";
        public QueryBuySellRequest(int id)
        {
            this.Id = id;
        }
    }
}
