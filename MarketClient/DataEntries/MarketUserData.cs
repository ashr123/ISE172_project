using System;
using System.Collections.Generic;
using System.Linq;
using MarketClient.DataEntries;
using MarketClient.Utils;

namespace MarketClient.DataEntries
{
    public class MarketUserData
    {
        Dictionary<string, int> Commodities { get; set; }
        int Funds { get; set; }
        LinkedList<int> Requests { get; set; }

        public override string ToString()
        {
            return "Commodities: "+Commodities+", Funds: "+Funds+", Requests: "+Requests;
        }
    }
}