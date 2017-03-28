using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketClient;
using MarketClient.DataEntries;

namespace Presentation
{
 
	public class Presentation
	{
		static MarketClientClass client = new MarketClientClass(); //create client to use it's methods
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("\nHello, This is an Algo-trading market system.\nWe Support the following operations: ");
				Console.WriteLine("1.To open a new request (buy/sell)\n2.To cancel an exist request\n3.To ask a query \n4.Exit");
				String input = Console.ReadLine();

				IsLegalCombinedLoop(input, 4);

				switch (input) //there are 3 cases: buy/sell request, cancel request, ask a query
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

					default:    //if the user pressed unknown key

						IsLegalCombinedLoop(input, 3);
						break;
				}//switch



			}
		}//main

		//this function activated when illegal key pressed and prints to the screen the command to type a llegal key
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


        //this function activate - buy/sell request. it collect info from user - send it to Logic layer and prints an answer
		private static void CollectingInfoBUYSELL(string a)
		{
			//write more legallity checks
			int Commodity, Amount, Price;

			do
			{
				Console.WriteLine("Please enter Commodity");
				Commodity=Myconvert(Console.ReadLine());
				Console.WriteLine("Please enter Amount");
				Amount=Myconvert(Console.ReadLine());
				Console.WriteLine("Please enter Price");
				Price=Myconvert(Console.ReadLine());
			} while (Commodity==-1|Amount==-1|Price==-1);
			

			MarketBuySell IDbuysell;
			if (a.Equals('1')) //'1' means buy
			{
					IDbuysell=client.SendBuyRequest(Price, Commodity, Amount);
					Console.WriteLine(IDbuysell.ToString());
			}
			else      //means sell
			{
					IDbuysell=client.SendSellRequest(Price, Commodity, Amount);
					Console.WriteLine(IDbuysell.ToString());
			}

			return;
		}//collect info buy-sell request


        //this function activate - cancel request. it collect info from user - send it to Logic layer and prints an answer
        private static void CollectInfoCancelRequst()
		{
			int id;
			Console.WriteLine("Please enter the ID request you wish to cancel\n");
			do
				id=Myconvert(Console.ReadLine());  //force the user to give legal id (only nums)
			while
				(id==-1);

				bool ans = client.SendCancelBuySellRequest(id);  //call to Logic layer func

               if (ans==true)
					Console.WriteLine("Cancellation succeed.\n");
                else
                     Console.WriteLine("Cancellation failed.\n");


            //print answer

            return;
		}//collect info cancel request

		private static void CollectInfoQueryRequst(string a)
		{
			
			switch (a)
			{
				case "1":    //buysellquery
					int id;
					Console.WriteLine("Please enter the ID request\n");
					do
						id=Myconvert(Console.ReadLine());    //force the user to give legal id (only nums)
                    while (id==-1);

						MarketItemQuery data1 = client.SendQueryBuySellRequest(id);  //call to Logic layer func
						Console.WriteLine(data1.ToString());  //print data on a certain deal
					
					break;

				case "2":   //user query
						MarketUserData data2 = client.SendQueryUserRequest();
						Console.WriteLine(data2.ToString());
					break;

				case "3":   //market query
					int commodity;
					Console.WriteLine("Please enter the Commodity\n");
					do
						commodity=Myconvert(Console.ReadLine());   //force the user to give legal commidity (only nums)
                    while (commodity==-1);
                    
						MarketCommodityOffer data3 = client.SendQueryMarketRequest(commodity);   //call to Logic layer func
                        Console.WriteLine(data3.ToString());
					
					break;

			}//switch
			return;
		}//collect info query

		private static int Myconvert(string s)
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
