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
    public class UnitTest7
    {
        [TestMethod]
        public void TestFalseAmount()
        {

            MarketClientClass client = new MarketClientClass();

            MarketBuySell buyreq1 = client.SendBuyRequest(10, 1, -5);    //no such amount : -5
            Assert.IsNotNull(buyreq1.Error);

            MarketBuySell buyreq2 = client.SendBuyRequest(12, 4, 0);  //no such amount : 0
            Assert.IsNotNull(buyreq2.Error);

            MarketBuySell sellreq3 = client.SendSellRequest(24, 5, -8);   //no such amount : -8
            Assert.IsNotNull(sellreq3.Error);

            //Errors expected (not null)

        }
    }
}
