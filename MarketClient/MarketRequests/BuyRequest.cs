using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class BuyRequest
    {
        public int Commodity { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public String type;
        public BuyRequest(int commodity, int amount, int price)
        {
			type="Buy";
			this.Commodity=commodity;
            this.Amount=amount;
            this.Price=price;
        }
    }
}
