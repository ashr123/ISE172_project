﻿using DataTier.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace DataTier
{
	public class SimpleHTTPClient
    {
		//private const string Url="http://localhost";
		private const string Url = "http://ise172.ise.bgu.ac.il";
		//private const string Url="http://ise172.ise.bgu.ac.il:8008";
		private static string User = "user54";

		/// <summary>
		/// Send an object of type T1, @item, parsed as json string embedded with the 
		/// authentication token, that is build using @user and @token, 
		/// as an HTTP post request to server at address @url.
		/// This method parse the reserver response using JSON to object of type T2.
		/// </summary>
		/// <typeparam name="T1">Type of the data object to send</typeparam>
		/// <typeparam name="T2">Type of the return object</typeparam>
		/// <param name="url">address of the server</param>
		/// <param name="user">username for authentication data</param>
		/// <param name="token">token for authentication data</param>
		/// <param name="item">the data item to send in the reuqest</param>
		/// <returns>the server response parsed as T2 object in json format</returns>
		public static T2 SendPostRequest<T1,T2>(string url, string user, T1 item) where T2 : class 
        {
            var response=SendPostRequest(Url, User, item);
            return response == null ? null : FromJson<T2>(response);
        }

        /// <summary>
        /// Send an object of type T1, @item, parsed as json string embedded with the 
        /// authentication token, that is build using @user and @token, 
        /// as an HTTP post request to server at address @url.
        /// This method reutens the server response as is.
        /// </summary>
        /// <typeparam name="T1">Type of the data object to send</typeparam>
        /// <param name="url">address of the server</param>
        /// <param name="user">username for authentication data</param>
        /// <param name="token">token for authentication data</param>
        /// <param name="item">the data item to send in the reuqest</param>
        /// <returns>the server response</returns>
        public static string SendPostRequest<T1>(string url, string user, T1 item)
        {
			string responseContent="";
			int nonce = new Random().Next(Int32.MinValue, Int32.MaxValue);
			string token = SimpleCtyptoLibrary.CreateToken(nonce);
			//Trace.WriteLine("user==User: "+(user==User)+"\nuser.equals(User): "+(user.Equals(User)));
			var auth = new { user, token, nonce };
			JObject jsonItem = JObject.FromObject(item);
			jsonItem.Add("auth", JObject.FromObject(auth));
			StringContent content = new StringContent(jsonItem.ToString());
			using (var client = new HttpClient())
			{
				var result = client.PostAsync(Url, content).Result;
				responseContent=result?.Content?.ReadAsStringAsync().Result;
				try
				{
					return SimpleCtyptoLibrary.Decrypt(responseContent);
				}
#pragma warning disable CS0168 // Variable is declared but never used
				catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
				{
					return SendPostRequest<T1>(url, user, item);
				}
			}
		}

        private static T FromJson<T>(string response) where T : class 
        {
            if (response == null)
                return null;
            try
            {
                return JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
                {
                    Error=delegate 
					{
                       throw new JsonException(response);
                    }
                });
            }
            catch
            {
                throw new MarketException(response);
            }
        }
	}
}