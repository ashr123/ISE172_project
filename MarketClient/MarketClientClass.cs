using System;
using System.Collections.Generic;
using System.Linq;
using MarketClient.DataEntries;
using MarketClient.Utils;
using MarketClient.MarketRequests;

namespace MarketClient
{
    class MarketClientClass : IMarketClient
    {
		private const string Url = "http://localhost"; //"http://ise172.ise.bgu.ac.il";
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

        public int SendBuyRequest(int price, int commodity, int amount)
        {
            //throw new NotImplementedException();
            BuyRequest request=new BuyRequest(commodity, amount, price);
			string ans=client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			try
			{
				return Convert.ToInt32(ans);
			}
			catch
			{
				throw new MarketException(ans);
			}
        }

        public bool SendCancelBuySellRequest(int id)
        {
            CancelBuySellRequest request=new CancelBuySellRequest(id);
            string ans=client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            if (ans.Equals("Ok"))
                return true;
			Console.WriteLine(ans);
            return false;
            //throw new NotImplementedException();
        }

        public MarketItemQuery SendQueryBuySellRequest(int id)
        {
            QueryBuySellRequest request=new QueryBuySellRequest(id);
            return client.SendPostRequest<QueryBuySellRequest, MarketItemQuery>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            //throw new NotImplementedException();
        }

        public MarketCommodityOffer SendQueryMarketRequest(int commodity)
        {
            QueryMarketRequest request=new QueryMarketRequest(commodity);
            return client.SendPostRequest<QueryMarketRequest, MarketCommodityOffer>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            //throw new NotImplementedException();
        }

        public MarketUserData SendQueryUserRequest()
        {
            QueryUserRequest request=new QueryUserRequest();
            return client.SendPostRequest<QueryUserRequest, MarketUserData>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            //throw new NotImplementedException();
        }

        public int SendSellRequest(int price, int commodity, int amount)
        {
            SellRequest request=new SellRequest(commodity, amount, price);
			string ans=client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
			try
			{
				return Convert.ToInt32(ans);
			}
			catch
			{
				throw new MarketException(ans);
			}
			//throw new NotImplementedException();
		}
    }
}