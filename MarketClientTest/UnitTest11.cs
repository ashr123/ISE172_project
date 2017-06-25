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
    public class UnitTest11
    {
        [TestMethod]
        public void TestMinMaxSQL()
        {
            
            int avg, min, max;
            bool error = false;

            try
            {
                Random rnd = new Random();
                int rndCommodity = rnd.Next(0, 10);

                //same cmd- check if the range is legal

                avg = minMaxAvg.avgPrice(rndCommodity);

                min = minMaxAvg.minPrice(rndCommodity);

                max = minMaxAvg.maxPrice(rndCommodity);

                if (min > avg | max < avg)
                    error = true;
            }

            catch (Exception)
            {
                error = true;
            }

            Assert.IsFalse(error);


        }
    }
}
