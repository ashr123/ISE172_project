using System;

namespace DataTier.DataEntries
{
    public class MarketCommodityOffer
    {
		public Exception Error { get; set; }
        public int Ask { get; set; }
        public int Bid { get; set; }

		/// <summary>
		/// represrnting the object.
		/// </summary>
		/// <returns>a string representing an object.</returns>
		/// <exception cref="MarketException">error is throw in case of invalid request or invalid parameter.</exception>
		public override string ToString()
        {
			if (Error!=null)
				return Error.ToString();
            return "Ask: "+Ask+", Bid: "+Bid;
        }
    }
}