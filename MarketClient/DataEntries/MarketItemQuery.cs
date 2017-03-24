namespace MarketClient.DataEntries
{
    public class MarketItemQuery
    {
        int Price { get; set; }
        int Amount { get; set; }
        int Commodity { get; set; }
        string Type { get; set; }
        string User { get; set; }

        public override string ToString()
        {
            return "Price: "+Price+", Amount: "+Amount+", Commidity: "+Commodity+", Type: "+Type+", User: "+User;
        }
    }
}