using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    //Query about a user request
	public class QueryUserRequest
	{
		public readonly string type;
		public QueryUserRequest()
		{
			type="queryUser";
		}
    }
}
