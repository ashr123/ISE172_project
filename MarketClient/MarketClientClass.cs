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
		private const string Url="http://localhost";
		//private const string Url="http://ise172.ise.bgu.ac.il";
		//private const string Url="http://ise172.ise.bgu.ac.il:8008";
		private const string User="user54";
		private const string error=", Url: "+Url+", User: "+User;
		private const string PrivateKey=@"-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQC2VKy0OMXoFvuxGeP/n92VV3wIt2X/kIG2BhuY6WE+SrvUOuxR
4hH5FT7fFWR0kVPBHJmUwwu8egJo+D7UyYF0d7A0UjVzFL1t02OsPcUnIXWs0PlO
Nz+nbhDDB//IWyR5iJejwCrZt0fBpISPmlSxyjp+uThtdPX1JtSQVv7iHQIDAQAB
AoGBAJIkIl09mBsjuM9F0kKEr4VRHsCZxy5ldCIimSIiBWh5XD2KkPo8um0sQz1p
lx/7j+cb9lmPUCvcm2vpder2LESA+rVoLqMpTQFh1ynLAjYXT3HTN8ZRRxBYY3mA
Fg/UbtjSB098GFWH4AV3LOGPfYhNrsVsiuhz0nQX7ADYIjchAkEAuz1iEYCRyBJa
DkCJsIO6gbu8A4ezuAm00ppbcRHxlILDEarG5ABdo8X3Q7Sx5Vh69GBpD++6hMO2
0UzOEIvQ1QJBAPlJyhhVk79wN3bdv3VF5w7sHhV2Hl5gSR1SmD+r0+1oJVAY8iZ+
GKLlshK1JVw2x7B2SNsYKfDPj3idORDosCkCQQCmpMMbgKo+vtaXyKjDCPp9bHCx
U52INltQ9UBdKfMwkhC7MJtDYW/1ysN+5ttNm6oSxZu8K0h90RJsxUbBQy7hAkEA
8+MKMhZ/TvrFeKhnqJ8z9/hvUkXWXjTLM0HcK+a6lvieEKfnOFuDVNNuDTlmDLqX
UP/YNWmFltAqKDGBZBaSSQJAJI7KrB9m/C874oxqv54izkfKwjCpoD/OvZ0h61Yl
1e7E1sB495nH617WpM1fFEqAuZUgdhb33VGkty1xFsqyxQ==
-----END RSA PRIVATE KEY-----
";
		public AllMarketRequest QueryAllMarketRequest()
		{
			try
			{
				return new AllMarketRequest
				{
					MarketInfo=SimpleHTTPClient.SendPostRequest<QueryAllMarketRequest, List<ItemAskBid>>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new QueryAllMarketRequest())
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
					Requests=SimpleHTTPClient.SendPostRequest<QueryUserRequests, List<AllDataRequest>>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new QueryUserRequests())
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
					Id=Int32.Parse(SimpleHTTPClient.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new BuyRequest(commodity, amount, price)))
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
				ans=SimpleHTTPClient.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new CancelBuySellRequest(id));
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
				return SimpleHTTPClient.SendPostRequest<QueryBuySellRequest, MarketItemQuery>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new QueryBuySellRequest(id));
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
				return SimpleHTTPClient.SendPostRequest<QueryMarketRequest, MarketCommodityOffer>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new QueryMarketRequest(commodity));
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
				return SimpleHTTPClient.SendPostRequest<QueryUserRequest, MarketUserData>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new QueryUserRequest());
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
					Id=Int32.Parse(SimpleHTTPClient.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), new SellRequest(commodity, amount, price)))
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