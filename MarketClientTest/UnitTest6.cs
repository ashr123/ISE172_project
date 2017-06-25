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

            MarketBuySell buyreq1 = client.SendBuyRequest(-7, 1, 3);    //no such price : -7
            Assert.IsNotNull(buyreq1.Error);

            MarketBuySell buyreq2 = client.SendBuyRequest(2000000, 1, 1);  //no such price : 2,000,000
            Assert.IsNotNull(buyreq2.Error);

            MarketBuySell sellreq3 = client.SendSellRequest(-25, 5, 3);   //no such price : -25
            Assert.IsNotNull(sellreq3.Error);

            //Errors expected (not null)

        }
    }
}
