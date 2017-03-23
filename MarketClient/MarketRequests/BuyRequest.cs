using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class BuyRequest
    {
        int Commodity { get; set; }
        int Amount { get; set; }
        int Price { get; set; }
        String type { get; set; }
        public BuyRequest(int commodity, int amount, int price)
        {
            this.Commodity=commodity;
            this.Amount=amount;
            this.Price=price;
        }
    }
}
