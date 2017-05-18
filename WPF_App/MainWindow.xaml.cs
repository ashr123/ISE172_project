﻿using System;
using System.Windows;
using System.Windows.Controls;
using DataTier;
using DataTier.DataEntries;
using DataTier.Loggers;
using System.Collections.Generic;
using LogicTier;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private static readonly IMarketClient market = new MarketClientClass();
		public static MarketUserData UserData { get; set; }
		public static ObservableCollection<Record>  History { get; set; }
		public static ObservableCollection<MarketData> MarketData1 { get; set; }
		public static ObservableCollection<MarketRequests> MarketRequests1 { get; set; }
		private static DispatcherTimer timer = new DispatcherTimer()
		{
			Interval=TimeSpan.FromSeconds(10),
			IsEnabled=true
		};

		public void Updater()
		{
			AllMarketRequest MarketRequestsTemp = market.QueryAllMarketRequest();
			UserData=market.SendQueryUserRequest();
			MarketUserRequests MarketDataTemp=market.QueryUserRequests();
			MarketData1=new ObservableCollection<MarketData>();
			MarketRequests1=new ObservableCollection<MarketRequests>();
			foreach (AllDataRequest item in MarketDataTemp.Requests)
			{
				MarketRequests1.Add(new MarketRequests()
				{
					Id=item.Id,
					Type=item.Request.Type,
					Commodity=item.Request.Commodity,
					Amount=item.Request.Amount,
					Price=item.Request.Price
				});
			}
			foreach (ItemAskBid item in MarketRequestsTemp.MarketInfo)
				MarketData1.Add(new MarketData()
				{
					Id=item.Id,
					Ask=item.Info.Ask,
					Bid=item.Info.Bid,
				});
			History=HistoryLogger.ReadHistory();
			foreach (Record rec in History)
				rec.IsExecuted=!UserData.Requests.Contains(rec.RequestId);
			UpdateItemSources();
		}

		private void UpdateItemSources()
		{
			HistoryDataGrid.ItemsSource=History;
			ActiveRequest.ItemsSource=MarketRequests1;
			AskBidDataGrid.ItemsSource=MarketData1;
			UserDataLabel.Content=UserData;
		}

		public void OnTimedEvent(Object source, EventArgs e)
		{
			Updater();
			Trace.WriteLine("TTTTTTIIIIIIMMMMMMEEEEEERRRRRR");
		}

		public MainWindow()
        {
			InitializeComponent();
			DataContext=this;
			Updater();
			timer.Tick+=OnTimedEvent;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
			if (!(Int32.TryParse(SellCommodityField.Text, out int Commodity)))
				MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(SellPriceField.Text, out int Price)))
				MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(SellAmountField.Text, out int Amount)))
				MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (Commodity > 0 || Price > 0 || Amount > 0)
            {
                IMarketClient market = new MarketClientClass();
                MarketBuySell marketBuySell = market.SendBuyRequest(Price, Commodity, Amount);
                if (marketBuySell.Error == null)
                {
                    MessageBox.Show("Sucsess!! Your Buy request has been placed. your id is: " + marketBuySell.Id);
                    HistoryLogger.WriteHistory(marketBuySell.Id, "Buy", Commodity, Price, Amount);
					Updater();
                }
                else
					MessageBox.Show(marketBuySell.ToString());
            }
            else
            {
				MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(Int32.TryParse(SellCommodityField.Text, out int Commodity)))
                MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!(Int32.TryParse(SellPriceField.Text, out int Price)))
                MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!(Int32.TryParse(SellAmountField.Text, out int Amount)))
                MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (Commodity > 0 || Price > 0 || Amount > 0)
            {
                IMarketClient market = new MarketClientClass();
                MarketBuySell marketBuySell = market.SendSellRequest(Price, Commodity, Amount);
                if (marketBuySell.Error == null)
                {
                    MessageBox.Show("Sucsess!! Your Buy request has been placed. your id is: " + marketBuySell.Id);
                    HistoryLogger.WriteHistory(marketBuySell.Id, "Sell", Commodity, Price, Amount);
					Updater();
                }
                else
					MessageBox.Show(marketBuySell.ToString());
            }
            else
				MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AmaButton_Click(object sender, RoutedEventArgs e)
        {
			if (ManualAMAButton.IsEnabled)
			{
				AMA.TimerOfAMA(true);
				ManualAMAButton.IsEnabled=false;
				BuyButton.IsEnabled=false;
				SellButton.IsEnabled=false;
				AMAPriceField.IsEnabled=false;
				AMAAmountField.IsEnabled=false;
				AMACommodityField.IsEnabled=false;
				AMAbuyORsellField.IsEnabled=false;
				SellPriceField.IsEnabled=false;
				SellAmountField.IsEnabled=false;
				SellCommodityField.IsEnabled=false;
			}
			else
			{
				AMA.TimerOfAMA(false);
				ManualAMAButton.IsEnabled=true;
				BuyButton.IsEnabled=true;
				SellButton.IsEnabled=true;
				AMAPriceField.IsEnabled=true;
				AMAAmountField.IsEnabled=true;
				AMACommodityField.IsEnabled=true;
				AMAbuyORsellField.IsEnabled=true;
				SellPriceField.IsEnabled=true;
				SellAmountField.IsEnabled=true;
				SellCommodityField.IsEnabled=true;
			}
		}

		public class MarketData
		{
			public int Id { get; set; }
			public int Ask { get; set; }
			public int Bid { get; set; }
		}
		public class MarketRequests
		{
			public int Id { get; set; }
			public string Type { get; set; }
			public int Commodity { get; set; }
			public int Amount { get; set; }
			public int Price { get; set; }
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			market.SendCancelBuySellRequest(int.Parse(((Button)sender).CommandParameter.ToString()));
			//MessageBox.Show(market.SendCancelBuySellRequest(int.Parse(((Button)sender).CommandParameter.ToString())).ToString(), "Approval", MessageBoxButton.OK, MessageBoxImage.Information);
			foreach (MarketRequests item in MarketRequests1)
				if (item.Id==int.Parse(((Button)sender).CommandParameter.ToString()))
				{
					HistoryLogger.WriteHistory(item.Id, "Delete", item.Commodity, item.Price, item.Amount);
					break;
				}
			Updater();
			Trace.WriteLine(int.Parse(((Button)sender).CommandParameter.ToString()));
		}

		private void BuyButton_Click_1(object sender, RoutedEventArgs e)
		{
			if (!(Int32.TryParse(SellCommodityField.Text, out int Commodity)))
				MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(SellPriceField.Text, out int Price)))
				MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(SellAmountField.Text, out int Amount)))
				MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (Commodity>0||Price>0||Amount>0)
			{
				IMarketClient market = new MarketClientClass();
				MarketBuySell marketBuySell = market.SendBuyRequest(Price, Commodity, Amount);
				if (marketBuySell.Error==null)
				{
					MessageBox.Show("Sucsess!! Your Buy request has been placed. your id is: "+marketBuySell.Id);
					HistoryLogger.WriteHistory(marketBuySell.Id, "Sell", Commodity, Price, Amount);
					Updater();
				}
				else
					MessageBox.Show(marketBuySell.ToString());
			}
			else
				MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}