using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.MarketRequests
{
    //Buy Request class
    public class BuyRequest
    {
		public int commodity;
		public int amount;
		public int price;
        public readonly string type;
        public BuyRequest(int commodity, int amount, int price)
        {
			type="buy";
			this.commodity=commodity;
            this.amount=amount;
            this.price=price;
        }
    }
}
