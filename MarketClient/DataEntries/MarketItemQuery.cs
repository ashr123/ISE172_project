namespace DataTier.DataEntries
{
    public class MarketItemQuery
    {
		public string Error { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Commodity { get; set; }
        public string Type { get; set; }
        public string User { get; set; }

		/// <summary>
		/// represrnting the object.
		/// </summary>
		/// <returns>a string representing an object.</returns>
		/// <exception cref="MarketException">error is throw in case of invalid request or invalid parameter.</exception>
		public override string ToString()
        {
			if (Error!=null)
				return Error;
            return "Price: "+Price+", Amount: "+Amount+", Commidity: "+Commodity+", Type: "+Type+", User: "+User;
        }
    }
}