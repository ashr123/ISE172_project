using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class QueryBuySellRequest
    {
        int Id { get; set; }
        public string type;
        public QueryBuySellRequest(int id)
        {
			type="queryBuySell";
			this.Id=id;
        }
    }
}
