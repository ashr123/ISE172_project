using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTier;
using DataTier.Utils;

namespace DataTierTest
{
    [TestClass]
    public class UnitTest2
    {
        
		[TestMethod]
		public void TestQueryAllMarketRequest()
		{
			Assert.IsNull(new MarketClientClass().QueryUserRequests().Error);
		}
	}
}