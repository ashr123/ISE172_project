using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTier;
using DataTier.Utils;
using DataTier.DataEntries;
using LogicTier;

namespace MarketClientTest
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void TestFalsePrice()
        {



            MarketClientClass client = new MarketClientClass();

            MarketBuySell sellreq1 = client.SendSellRequest(2000000, 2, 1);  //no such price : 2,000,000
            Assert.IsNotNull(sellreq1.Error);

            MarketBuySell sellreq2 = client.SendSellRequest(-25, 5, 3);   //no such price : -25
            Assert.IsNotNull(sellreq2.Error);

            //Errors expected (not null)

        }
    }
}
