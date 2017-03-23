using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.Market_Requests
{
    class QuerySell
    {
        int Id { get; set; }
        
        public QuerySell(int id)
        {
            this.Id = id;
        }
    }
}
