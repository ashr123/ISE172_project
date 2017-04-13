using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.MarketRequests
{
    //Query about buy or sell request
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
