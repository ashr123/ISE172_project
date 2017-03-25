using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class QueryBuySellRequest
    {
		public int id;
        public readonly string type;
        public QueryBuySellRequest(int id)
        {
			type="queryBuySell";
			this.id=id;
        }
    }
}
