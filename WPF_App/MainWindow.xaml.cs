﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTier;
using DataTier.DataEntries;
using System.Timers;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private static readonly IMarketClient market = new MarketClientClass();
		public static string UserData { get; set; }

		private static void OnTimedEvent(Object source, ElapsedEventArgs e)
		{
			UserData=market.SendQueryUserRequest().ToString();
		}

		public MainWindow()
        {
			InitializeComponent();
			DataContext=this;
			UserData=market.SendQueryUserRequest().ToString();
			Timer UserDataUpdater = new Timer(10000)
			{
				AutoReset=true,
				Enabled=true
			};
			UserDataUpdater.Elapsed+=OnTimedEvent;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            Page myPage = new BuyPage();
            this.Content = myPage;
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            Page myPage = new SellPage();
            this.Content = myPage;
        }

        private void HisrtoryButton_Click(object sender, RoutedEventArgs e)
        {
            Page myPage = new HistoryPage();
            this.Content = myPage;
        }
    }
}