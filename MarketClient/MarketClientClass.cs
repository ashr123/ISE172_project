using System;
using System.Collections.Generic;
using System.Linq;
using MarketClient.DataEntries;
using MarketClient.Utils;

namespace MarketClient
{
    class MarketClientClass : IMarketClient
    {
        private const string Url = "http://ise172.ise.bgu.ac.il";
        private const string User = "user54";
        private const string PrivateKey = "MIICXgIBAAKBgQC2VKy0OMXoFvuxGeP/n92VV3wIt2X/kIG2BhuY6WE+SrvUOuxR4hH5FT7fFWR0kVPBHJmUwwu8egJo+D7UyYF0d7A0UjVzFL1t02OsPcUnIXWs0PlONz+nbhDDB//IWyR5iJejwCrZt0fBpISPmlSxyjp+uThtdPX1JtSQVv7iHQIDAQABAoGBAJIkIl09mBsjuM9F0kKEr4VRHsCZxy5ldCIimSIiBWh5XD2KkPo8um0sQz1plx/7j+cb9lmPUCvcm2vpder2LESA+rVoLqMpTQFh1ynLAjYXT3HTN8ZRRxBYY3mAFg/UbtjSB098GFWH4AV3LOGPfYhNrsVsiuhz0nQX7ADYIjchAkEAuz1iEYCRyBJaDkCJsIO6gbu8A4ezuAm00ppbcRHxlILDEarG5ABdo8X3Q7Sx5Vh69GBpD++6hMO20UzOEIvQ1QJBAPlJyhhVk79wN3bdv3VF5w7sHhV2Hl5gSR1SmD+r0+1oJVAY8iZ+GKLlshK1JVw2x7B2SNsYKfDPj3idORDosCkCQQCmpMMbgKo+vtaXyKjDCPp9bHCxU52INltQ9UBdKfMwkhC7MJtDYW/1ysN+5ttNm6oSxZu8K0h90RJsxUbBQy7hAkEA8+MKMhZ/TvrFeKhnqJ8z9/hvUkXWXjTLM0HcK+a6lvieEKfnOFuDVNNuDTlmDLqXUP/YNWmFltAqKDGBZBaSSQJAJI7KrB9m/C874oxqv54izkfKwjCpoD/OvZ0h61Yl1e7E1sB495nH617WpM1fFEqAuZUgdhb33VGkty1xFsqyxQ==";
        SimpleHTTPClient client = new SimpleHTTPClient();
        public int SendBuyRequest(int price, int commodity, int amount)
        {
            //throw new NotImplementedException();
            var request = new { type = "buy", price = price, commodity = commodity, amount = amount };
            return Convert.ToInt32(client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request));
        }

        public bool SendCancelBuySellRequest(int id)
        {
            var request = new { type = "cancelBuySell", id = id };
            string ans = client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            if (ans.Equals("OK"))
                return true;
            return false;
            //throw new NotImplementedException();
        }

        public IMarketItemQuery SendQueryBuySellRequest(int id)
        {
            var request = new { type = "queryBuySell", id = id };
            return client.SendPostRequest<object, IMarketItemQuery>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            //throw new NotImplementedException();
        }

        public IMarketCommodityOffer SendQueryMarketRequest(int commodity)
        {
            var request = new { type = "queryMarket", commodity = commodity };
            return client.SendPostRequest<object, IMarketCommodityOffer>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            //throw new NotImplementedException();
        }

        public IMarketUserData SendQueryUserRequest()
        {
            var request = new { type = "queryUser" };
            return client.SendPostRequest<object, IMarketUserData>(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request);
            /*Dictionary<string, int> d = new Dictionary<string, int>();
           d["ddd"]=5;
            foreach (string s in d.Keys)
            {
                d[s];
            }*/
            //throw new NotImplementedException();
        }

        public int SendSellRequest(int price, int commodity, int amount)
        {
            var request = new { type = "sell", price = price, commodity = commodity, amount = amount };
            return Convert.ToInt32(client.SendPostRequest(Url, User, SimpleCtyptoLibrary.CreateToken(User, PrivateKey), request));
            //throw new NotImplementedException();
        }
    }
}