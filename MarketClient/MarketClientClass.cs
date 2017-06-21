using System;
using System.Collections.Generic;
using System.Linq;
using DataTier.DataEntries;
using DataTier.Utils;
using DataTier.MarketRequests;
using log4net;

namespace DataTier
{
	public class MarketClientClass : IMarketClient
	{
		private static readonly ILog myLogger=LogManager.GetLogger("Debug");
		//private const string Url="http://localhost";
		private const string Url="http://ise172.ise.bgu.ac.il";
		//private const string Url="http://ise172.ise.bgu.ac.il:8008";
		private const string User="user54";
		private const string error=", Url: "+Url+", User: "+User;
		public AllMarketRequest QueryAllMarketRequest()
		{
			try
			{
				return new AllMarketRequest
				{
					MarketInfo=SimpleHTTPClient.SendPostRequest<QueryAllMarketRequest, List<ItemAskBid>>(Url, User, new QueryAllMarketRequest())
				};
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error);
				return new AllMarketRequest { Error=e };
			}
		}

		public MarketUserRequests QueryUserRequests()
		{
			try
			{
				return new MarketUserRequests
				{
					Requests=SimpleHTTPClient.SendPostRequest<QueryUserRequests, List<AllDataRequest>>(Url, User, new QueryUserRequests())
				};
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error);
				return new MarketUserRequests { Error=e };
			}
		}

		public MarketBuySell SendBuyRequest(int price, int commodity, int amount)
		{
			try
			{
				return new MarketBuySell
				{
					Id=Int32.Parse(SimpleHTTPClient.SendPostRequest(Url, User, new BuyRequest(commodity, amount, price)))
				};
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error+", Price: "+price+", Commodity: "+commodity+", Amount: "+amount);
				return new MarketBuySell { Error=e };
			}
		}

		public bool SendCancelBuySellRequest(int id)
		{
			string ans="";
			try
			{
				ans=SimpleHTTPClient.SendPostRequest(Url, User, new CancelBuySellRequest(id));
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error+", ID: "+id);
				return false;
			}
			if (ans.Equals("Ok"))
				return true;
			myLogger.Debug("Exeption: "+ans+", ID: "+id);
			return false;
		}

		public MarketItemQuery SendQueryBuySellRequest(int id)
		{
			try
			{
				return SimpleHTTPClient.SendPostRequest<QueryBuySellRequest, MarketItemQuery>(Url, User, new QueryBuySellRequest(id));
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error+", ID: "+id);
				return new MarketItemQuery { Error=e };
			}
		}

		public MarketCommodityOffer SendQueryMarketRequest(int commodity)
		{
			try
			{
				return SimpleHTTPClient.SendPostRequest<QueryMarketRequest, MarketCommodityOffer>(Url, User, new QueryMarketRequest(commodity));
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error+", Commodity: "+commodity);
				return new MarketCommodityOffer { Error=e };
			}
		}

		public MarketUserData SendQueryUserRequest()
		{
			try
			{
				return SimpleHTTPClient.SendPostRequest<QueryUserRequest, MarketUserData>(Url, User, new QueryUserRequest());
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error);
				return new MarketUserData { Error=e };
			}
		}

		public MarketBuySell SendSellRequest(int price, int commodity, int amount)
		{
			try
			{
				return new MarketBuySell
				{
					Id=Int32.Parse(SimpleHTTPClient.SendPostRequest(Url, User, new SellRequest(commodity, amount, price)))
				};
			}
			catch (Exception e)
			{
				myLogger.Error("Exeption: "+e.Message+error+", Price: "+price+", Commodity: "+commodity+", Amount: "+amount);
				return new MarketBuySell { Error=e };
			}
		}
	}
}