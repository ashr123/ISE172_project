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
    public class UnitTest8
    {
        [TestMethod]
        public void TestAMAtimers()
        {
            AMA.ResetBothTimers();

            bool amaTimerIsSetted = AMA.checkAmaAutoTimerIsSet();
            Assert.AreEqual<bool>(true, amaTimerIsSetted);

            bool userTimerIsSetted = AMA.checkUserAmaTimerIsSet();
            Assert.AreEqual<bool>(true, userTimerIsSetted);

        }
    }
}
