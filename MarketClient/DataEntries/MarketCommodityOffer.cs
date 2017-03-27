namespace MarketClient.DataEntries
{
    public class MarketCommodityOffer
    {
        public int Ask { get; set; }
        public int Bid { get; set; }

        public override string ToString()
        {
            return "Ask: "+Ask+", Bid: "+Bid;
        }
    }
}