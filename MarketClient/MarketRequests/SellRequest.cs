using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.Market_Requests
{
    class SellRequest
    {
        int Commodity { get; set; }
        int Amount { get; set; }
        int Price { get; set; }
    }
}
