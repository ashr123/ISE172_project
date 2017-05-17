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
using DataTier.Loggers;
using DataTier.DataEntries;
using DataTier;

namespace WPF_App
{
	/// <summary>
	/// Interaction logic for HistoryPage.xaml
	/// </summary>
	public partial class HistoryPage : Page
	{
		public List<Record> History { get; set; }
		//public List<int> UserActiveRequests { get; set; }
		public HistoryPage()
        {
			InitializeComponent();
			History=HistoryLogger.ReadHistory();
			//UserActiveRequests=new MarketClientClass().SendQueryUserRequest().Requests;
			DataContext =this;
        }
	}
}