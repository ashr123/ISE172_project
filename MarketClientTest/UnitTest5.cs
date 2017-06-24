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
    public class UnitTest5
    {
        [TestMethod]
        public void TestFalseCommodity()
        {
            
            MarketClientClass client = new MarketClientClass();
            
            MarketBuySell buyreq1 = client.SendBuyRequest(5, -2, 3);
            Assert.IsNotNull(buyreq1.Error);

            MarketBuySell buyreq2 = client.SendBuyRequest(5, 10, 3);
            Assert.IsNotNull(buyreq2.Error);

            MarketBuySell buyreq3 = client.SendBuyRequest(5, 100, 3);
            Assert.IsNotNull(buyreq3.Error);


        }
    }
}
