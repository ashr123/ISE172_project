﻿using System;

namespace DataTier.Utils
{
    public class MarketException : Exception
    {
        public MarketException()
        {
        }

        public MarketException(string message) : base(message)
        {
        }
    }

}