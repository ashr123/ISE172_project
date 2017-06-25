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
    public class UnitTest9
    {
        [TestMethod]
        public void TestBuySellActions()
        {

            bool error = false;
            try
            {
                MarketClientClass client = new MarketClientClass();

                MarketBuySell buyreq1 = client.SendBuyRequest(1, 1, 3);   //legal req
                Assert.IsNull(buyreq1.Error);                     //null expected
                client.SendCancelBuySellRequest(buyreq1.Id);


                MarketBuySell sellreq1 = client.SendBuyRequest(1, 1, 3);    //legal req
                Assert.IsNull(sellreq1.Error);                       //null expected
                client.SendCancelBuySellRequest(sellreq1.Id);

                //no errors expected ( null)
            }

            catch (Exception)
            {
                error = true;
            }

            Assert.IsFalse(error);                       //false expected


        }
    }
}
