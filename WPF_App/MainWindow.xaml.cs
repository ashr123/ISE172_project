using System;
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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

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
		public static ObservableCollection<UserAsksLink> UserAsks { get; set; }
		private static DispatcherTimer timer=new DispatcherTimer
		{
			Interval=TimeSpan.FromSeconds(10),
			IsEnabled=true
		};

		public void Updater()
		{
			AllMarketRequest MarketRequestsTemp=market.QueryAllMarketRequest();
			UserData=market.SendQueryUserRequest();
			MarketUserRequests MarketDataTemp=market.QueryUserRequests();
			MarketData1=new ObservableCollection<MarketData>();
			MarketRequests1=new ObservableCollection<MarketRequests>();
			foreach (AllDataRequest item in MarketDataTemp.Requests)
				MarketRequests1.Add(new MarketRequests
				{
					Id=item.Id,
					Type=item.Request.Type,
					Commodity=item.Request.Commodity,
					Amount=item.Request.Amount,
					Price=item.Request.Price
				});
			foreach (ItemAskBid item in MarketRequestsTemp.MarketInfo)
				MarketData1.Add(new MarketData
				{
					Id=item.Id,
					Ask=item.Info.Ask,
					Bid=item.Info.Bid,
				});
			History=HistoryLogger.ReadHistory();
			foreach (Record rec in History)
				rec.IsExecuted=!UserData.Requests.Contains(rec.RequestId);
			History=new ObservableCollection<Record>(History.OrderByDescending(a => a.Time));
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
			UserAsks=new ObservableCollection<UserAsksLink>();
			UserAsksDataGrid.ItemsSource=UserAsks;
			BuySell.Items.Add("Buy");
			BuySell.Items.Add("Sell");
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
				MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
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

        private void AmaButton_Click(object sender, RoutedEventArgs e)
		{
			if (ManualAMAButton.IsEnabled)
			{
				AMA.TimerOfAMA(true);
				AmaButton.Content="Stop automatic AMA";
			}
			else
			{
				AMA.TimerOfAMA(false);
				AmaButton.Content="Start automatic AMA";
			}
			EnableDisableControls();
			ManualAMAButton.IsEnabled=!ManualAMAButton.IsEnabled;
		}

		private void EnableDisableControls()
		{
			ActiveRequest.IsEnabled=!ActiveRequest.IsEnabled;
			BuyButton.IsEnabled=!BuyButton.IsEnabled;
			SellButton.IsEnabled=!SellButton.IsEnabled;
			AMAPriceField.IsEnabled=!AMAPriceField.IsEnabled;
			AMAAmountField.IsEnabled=!AMAAmountField.IsEnabled;
			AMACommodityField.IsEnabled=!AMACommodityField.IsEnabled;
			SellPriceField.IsEnabled=!SellPriceField.IsEnabled;
			SellAmountField.IsEnabled=!SellAmountField.IsEnabled;
			SellCommodityField.IsEnabled=!SellCommodityField.IsEnabled;
			UserAsksDataGrid.IsEnabled=!UserAsksDataGrid.IsEnabled;
			BuySell.IsEnabled=!BuySell.IsEnabled;
			ManualAmaAdder.IsEnabled=!ManualAmaAdder.IsEnabled;
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

		private void CancelRequestButton_Click(object sender, RoutedEventArgs e)
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

		private void CancelRequestButton_Click_1(object sender, RoutedEventArgs e)
		{
			foreach (UserAsksLink item in UserAsks)
				if (item.Commodity==int.Parse(((Button)sender).CommandParameter.ToString()))
				{
					UserAsks.Remove(item);
					return;
				}
		}

		private void ManualAMAButton_Click(object sender, RoutedEventArgs e)
		{
			if (ManualAMAButton.IsEnabled)
			{
				AMA.TimerOfAutoUser(new List<UserAsksLink>( UserAsks));
				ManualAMAButton.Content="Stop manual AMA";
			}
			else
			{
				AMA.ResetBothTimers();
				ManualAMAButton.Content="Start manual AMA";
			}
			EnableDisableControls();
			AmaButton.IsEnabled=!AmaButton.IsEnabled;
		}

		private void ManualAmaAdder_Click(object sender, RoutedEventArgs e)
		{
			if (!(Int32.TryParse(AMACommodityField.Text, out int Commodity)))
				MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(AMAPriceField.Text, out int Price)))
				MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(AMAAmountField.Text, out int Amount)))
				MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (Commodity>0||Price>0||Amount>0)
			{
				UserAsks.Add(new UserAsksLink
				{
					Commodity=Commodity,
					DesiredPrice=Price,
					Amount=Amount,
					BuyORsell=BuySell.SelectionBoxItem.ToString().Equals("Buy") ? true : false
				});
			}
			else
				MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}