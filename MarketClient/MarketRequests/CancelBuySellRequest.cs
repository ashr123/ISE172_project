using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{//gfjfjfjfjfjhfhhjjfh
    class CancelBuySellRequest
    {
        int Id { get; set; }
        String Type { get; set; }
        public CancelBuySellRequest(int id)
        {
            this.Id = id;
        }
    }
}
