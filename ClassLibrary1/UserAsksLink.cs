﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTier
{
    public class UserAsksLink
    {
        public int Commodity { set; get; }
		public bool BuyORsell { set; get; }    //buy=true . sell=false
		public int DesiredPrice { set; get; }
        public int Amount { set; get; }
    }
}