using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class SellRequest
    {
		public string type;
        int Commodity { get; set; }
        int Amount { get; set; }
        int Price { get; set; }

        public SellRequest(int commodity, int amount, int price)
        {
			type="sell";
			this.Commodity=commodity;
            this.Amount=amount;
            this.Price=price;
        }
    }
}
