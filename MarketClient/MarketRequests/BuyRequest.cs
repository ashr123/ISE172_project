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
        public BuyRequest(int price, int commodity, int amount)
        {
            Commodity=commodity;
            Amount=amount;
            Price=price;
        }
    }
}
