namespace MarketClient.DataEntries
{
    public class MarketCommodityOffer
    {
        int Ask { get; set; }
        int Bid { get; set; }

        public override string ToString()
        {
            return "Ask: "+", Bid: "+Bid;
        }
    }
}