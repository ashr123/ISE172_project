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
            
            MarketBuySell buyreq1 = client.SendBuyRequest(5, -2, 3);   //no such commodies : -2 
            Assert.IsNotNull(buyreq1.Error);

            MarketBuySell buyreq2 = client.SendBuyRequest(1, 10, 3);    //no such commodies : 10
            Assert.IsNotNull(buyreq2.Error);

            MarketBuySell sellreq3 = client.SendSellRequest(7, 24, 3);   //no such commodies : 24 
            Assert.IsNotNull(sellreq3.Error);

            //Errors expected (not null)
        }
    }
}
