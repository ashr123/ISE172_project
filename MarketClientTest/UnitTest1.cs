﻿using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTier;
using DataTier.Utils;

namespace DataTierTest
{
    [TestClass]
    public class UnitTest1
    {
        // fill those variable with our own data
        private const string Url="TEST";
        private const string User="TEST";
        private const string PrivateKey="TEST";


        [TestMethod]
        public void TestSimpleHTTPPost()
        {
            // Attantion!, this code is not good practice! this was made for the sole purpose of being an example.
            // A real good code, should use defined classes and and not hardcoded values!
            var request=new{
                type="queryUser",
            };
            //string response=SimpleHTTPClient.SendPostRequest(Url,User,SimpleCtyptoLibrary.CreateToken(User,PrivateKey), request);
            //Trace.Write($"Server response is: {response}");
        }

        [TestMethod]
        public void TestObjectBasedHTTPPost()
        {
            // This test query a diffrent site (not the MarketServer)! it's only for demostration.
            // this site doenst accept authentication, it only returns objects.
            //string url="http://ip.jsontest.com/";
            //IpAddress ip=new IpAddress {Ip="8.8.8.8"};
            //IpAddress response=SimpleHTTPClient.SendPostRequest<IpAddress,IpAddress>(url, null, null, ip);
            //Assert.IsNotNull(response);
            //Assert.IsNotNull(response.Ip);
            //Trace.Write($"Server response is: {response.Ip}");
        }

        private class IpAddress
        {
            public string Ip { get; set; }
        }

		[TestMethod]
		public void TestQueryAllMarketRequest()
		{
			Assert.IsNull(new MarketClientClass().QueryUserRequests().Error);
		}
	}
}