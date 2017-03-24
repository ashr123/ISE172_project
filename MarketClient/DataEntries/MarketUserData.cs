using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketClient.DataEntries
{
    public class MarketUserData
    {
        public Dictionary<int, int> Commodities { get; set; }
        public int Funds { get; set; }
        public LinkedList<int> Requests { get; set; }

        public override string ToString()
        {
			string output = "Commodities: [";
			foreach (int i in Commodities.Keys)
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
				if (i!=Requests.Last.Value)
					output+=", ";
			}
			return output+="]";
        }
    }
}