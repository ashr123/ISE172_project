using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class CancelSellRequest
    {
        int Id { get; set; }

        public CancelSellRequest(int id)
        {
            this.Id = id;
        }
    }
}
