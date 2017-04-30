using System;
using System.Collections.Generic;
using System.Linq;
using DataTier.DataEntries;
using DataTier.Utils;
using DataTier.MarketRequests;

namespace DataTier
{
    public class MarketClientClass : IMarketClient
    {
		//private const string Url = "http://localhost";
		private const string Url = "http://ise172.ise.bgu.ac.il";
		private const string User = "user54";
        private const string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
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
        SimpleHTTPClient client=new SimpleHTTPClient();

		public AllMarketRequest QueryAllMarketRequest()
		{
			QueryAllMarketRequest request = new QueryAllMarketRequest();
			try
			{
				return client.SendPostRequest<QueryAllMarketRequest, AllMarketRequest>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch (Exception e)
			{
				return new AllMarketRequest { Error=e };
			}
		}

		public MarketUserRequests QueryUserRequests()
		{
			QueryUserRequests request = new QueryUserRequests();
			try
			{
				return client.SendPostRequest<QueryUserRequests, MarketUserRequests>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch(Exception e)
			{
				return new MarketUserRequests { Error=e };
			}
		}

		public MarketBuySell SendBuyRequest(int price, int commodity, int amount)
        {
            BuyRequest request=new BuyRequest(commodity, amount, price);
			//Console.WriteLine(ans);
			try
			{
				return client.SendPostRequest<BuyRequest, MarketBuySell>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch (Exception e)
			{
				return new MarketBuySell { Error=e };
			}
        }

        public bool SendCancelBuySellRequest(int id)
        {
            CancelBuySellRequest request=new CancelBuySellRequest(id);
            string ans=client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			//Console.WriteLine(ans);
			if (ans.Equals("Ok"))
                return true;
			Console.WriteLine(ans);
            return false;
        }

        public MarketItemQuery SendQueryBuySellRequest(int id)
        {
            QueryBuySellRequest request=new QueryBuySellRequest(id);
			//Console.WriteLine(client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request));
			try
			{
				return client.SendPostRequest<QueryBuySellRequest, MarketItemQuery>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch (Exception e)
			{
				return new MarketItemQuery { Error=e };
			}
        }

        public MarketCommodityOffer SendQueryMarketRequest(int commodity)
        {
            QueryMarketRequest request=new QueryMarketRequest(commodity);
			//Console.WriteLine(client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request));
			try
			{
				return client.SendPostRequest<QueryMarketRequest, MarketCommodityOffer>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch (Exception e)
			{
				return new MarketCommodityOffer { Error=e };
			}
        }

        public MarketUserData SendQueryUserRequest()
        {
            QueryUserRequest request=new QueryUserRequest();
			Console.WriteLine(client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request));
			try
			{
				return client.SendPostRequest<QueryUserRequest, MarketUserData>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			}
			catch(Exception e)
			{
				return new MarketUserData { Error=e};
			}
        }

        public MarketBuySell SendSellRequest(int price, int commodity, int amount)
        {
            SellRequest request=new SellRequest(commodity, amount, price);
			try
			{
				return client.SendPostRequest<SellRequest, MarketBuySell>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request); ;
			}
			catch (Exception e)
			{
				return new MarketBuySell { Error=e };
			}
		}
    }
}