using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class CancelBuyRequest
    {
        int Id { get; set; }

        public CancelBuyRequest(int id)
        {
            this.Id = id;
        }
    }
}
