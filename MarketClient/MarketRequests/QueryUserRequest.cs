using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
	public class QueryUserRequest
	{
		public string type;
		public QueryUserRequest()
		{
			type="queryUser";
		}
    }
}
