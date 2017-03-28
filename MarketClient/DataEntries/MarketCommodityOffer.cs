namespace MarketClient.DataEntries
{
    public class MarketCommodityOffer
    {
		public string Error { get; set; }
        public int Ask { get; set; }
        public int Bid { get; set; }

        public override string ToString()
        {
			if (Error!=null)
				return Error;
            return "Ask: "+Ask+", Bid: "+Bid;
        }
    }
}