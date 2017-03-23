using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.Market_Requests
{
    class BuyRequest
    {
        int Commodity { get; set; }
        int Amount { get; set; }
        int Price { get; set; }

        public BuyRequest(int commodity, int amount, int price)
        {
            this.Commodity=commodity;
            this.Amount=amount;
            this.Price=price;
        }
    }
}
