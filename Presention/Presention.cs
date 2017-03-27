using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketClient;
using MarketClient.DataEntries;

namespace Presention
{
	public class Presention
	{

		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("\nHello, This is an Algo-trading market system.\n We Support the following operations: ");
				Console.WriteLine("1.To open a new request (buy/sell)\n2.To cancel an exist request\n3.To ask a query \n4.Exit");
				String input = Console.ReadLine();

				IsLegalCombinedLoop(input, 4);

				switch (input)
				{
					case "1":
						Console.WriteLine("You chose to open a new request.\n1.To buy \n2.To sell");
						string input1 = Console.ReadLine();
						IsLegalCombinedLoop(input1, 2);
						CollectingInfoBUYSELL(input1);
						break;

					case "2":
						Console.WriteLine("You chose to cancel a request.\n");
						CollectInfoCancelRequst();
						break;

					case "3":
						Console.WriteLine("You chose to ask a query.\n1.To buy/sellQuery\n2.To userQuery\n3.To marketQuery");
						string input3 = Console.ReadLine();
						IsLegalCombinedLoop(input3, 3);
						CollectInfoQueryRequst(input3);
						break;

					case "4":
						return;

					default:

						IsLegalCombinedLoop(input, 3);
						break;
				}//switch



			}
		}//main

		//this function activated when illegal key pressed
		private static void IsLegalCombinedLoop(String s, int options)
		{
			bool isLegal = CheckLegality(s, options);
			while (isLegal==false)
			{
				PrintWhenIllegalkey();
				s=Console.ReadLine();
				isLegal=CheckLegality(s, options);
			}
			return;
		}

		//only helping IsLegalCombinedLoop function
		private static bool CheckLegality(String s, int options)
		{

			if (s.Length!=1)
				return false;
			for (int i = 1; i<=options; i++)
				if (s.Equals(Convert.ToString(i)))
					return true;

			return false;
		}

		//only helping IsLegalCombinedLoop function
		private static void PrintWhenIllegalkey()
		{
			Console.WriteLine("You entered illegal input. Please press a legal key.");
			return;
		}

		private static void CollectingInfoBUYSELL(string a)
		{
			//write more legallity checks
			int Commodity, Amount, Price;

			do
			{
				Console.WriteLine("Please enter Commodity");
				Commodity=myconvert(Console.ReadLine());
				Console.WriteLine("Please enter Amount");
				Amount=myconvert(Console.ReadLine());
				Console.WriteLine("Please enter Price");
				Price=myconvert(Console.ReadLine());
			} while (Commodity==-1|Amount==-1|Price==-1);

			MarketClientClass client = new MarketClientClass(); //create client to use it's methoods

			int IDbuysell = 0;
			if (a.Equals('1')) //'1' means buy
			{
				try
				{
					IDbuysell=client.SendBuyRequest(Price, Commodity, Amount);
					Console.WriteLine("The request done successfully. The ID is "+IDbuysell+".");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

			}
			else      //means sell
			{
				try
				{
					IDbuysell=client.SendSellRequest(Price, Commodity, Amount);
					Console.WriteLine("The request done successfully. The ID is "+IDbuysell+".");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return;
		}//collect info buy-sell request

		private static void CollectInfoCancelRequst()
		{
			int id;
			Console.WriteLine("Please enter the ID request you wish to cancel\n");
			do
				id=myconvert(Console.ReadLine());
			while
				(id==-1);


			MarketClientClass client = new MarketClientClass(); //create client to use it's methoods
			try
			{
				bool ans = client.SendCancelBuySellRequest(id);

				if (ans==true)
					Console.WriteLine("Cancellation succeed.\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			//print answer

			return;
		}//collect info cancel request

		private static void CollectInfoQueryRequst(string a)
		{

			MarketClientClass client = new MarketClientClass(); //create client to use it's methoods


			switch (a)
			{
				case "1":
					int id;
					Console.WriteLine("Please enter the ID request\n");
					do
						id=myconvert(Console.ReadLine());
					while (id==-1);

					try
					{
						MarketItemQuery data1 = client.SendQueryBuySellRequest(id);
						Console.WriteLine("Buy-Sell query info is:\n"+data1.ToString());  //print data on a certain deal
					}

					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}

					break;

				case "2":
					try
					{
						MarketUserData data2 = client.SendQueryUserRequest();

						Console.WriteLine("User query info is:\n"+data2.ToString());
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					break;

				case "3":
					int commodity;
					Console.WriteLine("Please enter the Commodity\n");
					do
						commodity=myconvert(Console.ReadLine());
					while (commodity==-1);

					try
					{
						MarketCommodityOffer data3 = client.SendQueryMarketRequest(commodity);
						Console.WriteLine("Market query info is:\n"+data3.ToString());
					}

					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					break;

			}//switch
			return;
		}//collect info query

		private static int myconvert(string s)
		{
			try
			{
				return Convert.ToInt32(s);
			}
			catch
			{
				return -1;
			}
		}
	}//class 
}//namespace presention
