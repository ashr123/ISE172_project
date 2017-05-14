using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_App
{
    public enum Operation { Buy, Sell }
    public class BuySellObject
    {
        private Operation operation;
        private int Price { get; set; }
        private int Commodity { get; set; }
        private int Amount { get; set; }

        public BuySellObject(Operation operation, int Price, int Commodity, int Amount)
        {
            this.operation = operation;
            this.Price = Price;
            this.Commodity = Commodity;
            this.Amount = Amount;

        } 
    }
}
