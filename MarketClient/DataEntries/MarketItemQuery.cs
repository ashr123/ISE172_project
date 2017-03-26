namespace MarketClient.DataEntries
{
    public class MarketItemQuery
    {
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Commodity { get; set; }
        public string Type { get; set; }
        public string User { get; set; }

        public override string ToString()
        {
            return "Price: "+Price+", Amount: "+Amount+", Commidity: "+Commodity+", Type: "+Type+", User: "+User;
        }
    }
}