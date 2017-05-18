using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.MarketRequests
{
    //Cancel buy or sell request
    public class CancelBuySellRequest
    {
		public int id;
        public readonly string type;
        public CancelBuySellRequest(int id)
        {
			type="cancelBuySell";
			this.id=id;
        }
    }
}