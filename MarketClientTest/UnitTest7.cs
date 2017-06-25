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


            Random rnd = new Random();
            int rndCommodity1 = rnd.Next(0, 10);
            int rndCommodity2 = rnd.Next(0, 10);
            int rndCommodity3 = rnd.Next(0, 10);


            MarketBuySell sellreq1 = client.SendSellRequest(24, rndCommodity1, -8);   //no such amount : -8
            Assert.IsNotNull(sellreq1.Error);


            MarketBuySell sellreq2 = client.SendSellRequest(24, rndCommodity1, -3);   //no such amount : -3
            Assert.IsNotNull(sellreq2.Error);


            MarketBuySell sellreq3 = client.SendSellRequest(24, rndCommodity1, -100);   //no such amount : -100
            Assert.IsNotNull(sellreq3.Error);

            //Errors expected (not null)

        }
    }
}
