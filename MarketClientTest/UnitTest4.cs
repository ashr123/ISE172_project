using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTier;
using DataTier.DataEntries;

namespace MarketClientTest
{
	[TestClass]
	public class UnitTest4
	{
		[TestMethod]
		public void TestQueryAllMarketRequest()
		{
			Assert.IsNull(new MarketClientClass().QueryAllMarketRequest().Error);
		}

		[TestMethod]
		public void TestQueryUserRequests()
		{
			Assert.IsNull(new MarketClientClass().QueryUserRequests().Error);
		}

		[TestMethod]
		public void TestSendBuyRequest()
		{
			MarketUserData user = new MarketClientClass().SendQueryUserRequest();
			if (user.Funds<=0)
			{
				Assert.Fail();
				return;
			}
			MarketBuySell ans = new MarketClientClass().SendBuyRequest(1, 0, 1);
			Assert.IsNull(ans.Error);
			new MarketClientClass().SendCancelBuySellRequest(ans.Id);
		}

		[TestMethod]
		public void TestSendSellRequest()
		{
			MarketUserData user = new MarketClientClass().SendQueryUserRequest();
			for (int i = 0; i<user.Commodities.Count; i++)
				if (user.Commodities[i]>0)
				{
					MarketBuySell ans = new MarketClientClass().SendSellRequest(99, i, 1);
					Assert.IsNull(ans.Error);
					new MarketClientClass().SendCancelBuySellRequest(ans.Id);
					break;
				}
		}
	}
}