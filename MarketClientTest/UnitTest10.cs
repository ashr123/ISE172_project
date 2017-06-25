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
    public class UnitTest10
    {
        [TestMethod]
        public void TestLegalSQLoutput()
        {

            int avg, min, max;
            bool error = false;

            try
            {

                Random rnd = new Random();
                int rndCommodity1 = rnd.Next(0, 10);
                int rndCommodity2 = rnd.Next(0, 10);
                int rndCommodity3 = rnd.Next(0, 10);

                //different cmds- check if the values returned are non negeative

                avg = minMaxAvg.avgPrice(rndCommodity1);

                min = minMaxAvg.minPrice(rndCommodity2);

                max = minMaxAvg.maxPrice(rndCommodity3);

                if (avg <= 0 | min <= 0 | max <= 0)
                    error = true;
            }

            catch(Exception)
            {
                error = true;
            }

            Assert.IsFalse(error);

        }
    }
}
