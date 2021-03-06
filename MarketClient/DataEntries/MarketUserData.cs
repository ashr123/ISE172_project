﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTier.DataEntries
{
	public class MarketUserData
	{
		public Dictionary<int, int> Commodities { get; set; }
		public double Funds { get; set; }
        public List<int> Requests { get; set; }
		public Exception Error { get; set; }

		/// <summary>
		/// represrnting the object.
		/// </summary>
		/// <returns>a string representing an object.</returns>
		/// <exception cref="MarketException">error is throw in case of invalid request or invalid parameter.</exception>
		public override string ToString()
        {
			if (Error!=null)
				return Error.Message;
			string output = "Commodities: [";
			foreach (int i in Commodities.Keys)
			{
				output+=i+": "+Commodities[i];
				if (i!=Commodities.Keys.Last())
					output+=", ";
				else
					output+="]\nFunds: "+Funds+"\nRequests: [";
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