using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.DataEntries
{
	public class AllDataRequest
	{
		public MarketItemQuery Request { get; set; }
		public int Id { get; set; }

		public override string ToString()
		{
			return Request+", Id: "+Id;
		}
	}
}