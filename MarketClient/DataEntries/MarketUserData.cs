using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketClient.DataEntries
{
	public class MarketUserData
	{
		public Dictionary<string, int> Commodities { get; set; }
		public double Funds { get; set; }
        public List<int> Requests { get; set; }
		public string Error { get; set; }

		/// <summary>
		/// represrnting the object.
		/// </summary>
		/// <returns>a string representing an object.</returns>
		/// <exception cref="MarketException">error is throw in case of invalid request or invalid parameter.</exception>
		public override string ToString()
        {
			if (Error!=null)
				return Error;
			string output = "Commodities: [";
			foreach (string i in Commodities.Keys)
			{
				output+=i+": "+Commodities[i];
				if (i!=Commodities.Keys.Last())
					output+=", ";
				else
					output+="], Funds: "+Funds+", Requests: [";
			}
			foreach (int i in Requests)
			{
				output+=i;
				if (i!=Requests[Requests.Count-1])
					output+=", ";
			}
			return output+="]";
        }
    }
}