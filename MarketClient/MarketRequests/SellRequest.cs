using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class SellRequest
    {
		public readonly string type;
		public int commodity;
		public int amount;
		public int price;

        public SellRequest(int commodity, int amount, int price)
        {
			type="sell";
			this.commodity=commodity;
            this.amount=amount;
            this.price=price;
        }
    }
}
