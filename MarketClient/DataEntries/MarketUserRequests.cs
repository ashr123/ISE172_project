using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.DataEntries
{
	public class MarketUserRequests
	{
		public Exception Error { get; set; }
		public List<AllDataRequest> Requests { get; set; }

		public override string ToString()
		{
			if (Error!=null)
				return Error.Message;
			string output="[";
			foreach (AllDataRequest i in Requests)
			{
				output+=i;
				if (i!=Requests[Requests.Count-1])
					output+=", ";
			}
			return output+="]";
		}
	}
}