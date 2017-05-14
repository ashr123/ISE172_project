using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.DataEntries
{
	public class MarketBuySell
	{
		public Exception Error { get; set; }
		public int Id { get; set; }

		/// <summary>
		/// represrnting the object.
		/// </summary>
		/// <returns>a string representing an object.</returns>
		/// <exception cref="MarketException">error is throw in case of invalid request or invalid parameter.</exception>
		public override string ToString()
		{
			if (Error!=null)
				return Error.Message;
			return Convert.ToString(Id);
		}
	}
}
