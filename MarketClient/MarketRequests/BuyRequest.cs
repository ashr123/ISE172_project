﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketClient.MarketRequests
{
    public class BuyRequest
    {
		public int commodity;
		public int amount;
		public int price;
        public String type;
        public BuyRequest(int commodity, int amount, int price)
        {
			type="Buy";
			this.commodity=commodity;
            this.amount=amount;
            this.price=price;
        }
    }
}
