using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    class QueryBuy
    {
        int Id { get; set; }

        public QueryBuy(int id)
        {
            this.Id = id;
        }
    }
}
