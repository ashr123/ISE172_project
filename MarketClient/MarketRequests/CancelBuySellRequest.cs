using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class CancelBuySellRequest
    {
        int Id { get; set; }
        const string type = "cancelBuySell";
        public CancelBuySellRequest(int id)
        {
            this.Id = id;
        }
    }
}
