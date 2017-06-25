using System;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace WPF_App
{
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window2 : Window
	{
		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public Func<double, string> YFormatter { get; set; }
		private static DispatcherTimer timer = new DispatcherTimer
		{
			Interval=TimeSpan.FromSeconds(10),
			IsEnabled=true
		};

		public void OnTimedEvent(Object source, EventArgs e)
		{
			Trace.WriteLine("CCCCCCHHHHHHAAAAAARRRRRRTTTTTT");
			ObservableCollection<string> temp = new ObservableCollection<string>();
			using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
			{
				myConnection.Open();
				SqlCommand myCommand = new SqlCommand("select * from items where timestamp >= DATEADD(mi, -1, GETUTCDATE())", myConnection);
				SqlDataReader myDataReader = myCommand.ExecuteReader();
				Trace.WriteLine("myDataReader.HasRows: "+myDataReader.HasRows);
				if (myDataReader.HasRows)
					while (myDataReader.Read())
						temp.Add(myDataReader.GetDateTime(0).ToString());
			}
			Labels=new string[temp.Count];
			int i = 0;
			foreach (string s in temp)
				Labels[i++]=s;
			Chart.AxisX[0].Labels=Labels;

			SeriesCollection=new SeriesCollection
			{
				new LineSeries
				{
					Title = "Commodity 0",
					Values = new ChartValues<double>()
				},
				new LineSeries
				{
					Title = "Commodity 1",
					Values = new ChartValues<double>(),
					//PointGeometry = null
				},
				new LineSeries
				{
					Title = "Commodity 2",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 3",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 4",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 5",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 6",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 7",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 8",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
				new LineSeries
				{
					Title = "Commodity 9",
					Values = new ChartValues<double>(),
					//PointGeometry = DefaultGeometries.Square,
					//PointGeometrySize = 15
				},
			};

			using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
			{
				myConnection.Open();
				SqlCommand myCommand = new SqlCommand("select * from items where timestamp >= DATEADD(mi, -1, GETUTCDATE())", myConnection);
				SqlDataReader myDataReader = myCommand.ExecuteReader();
				Trace.WriteLine("myDataReader.HasRows: "+myDataReader.HasRows);
				if (myDataReader.HasRows)
					while (myDataReader.Read())
					{
						//Trace.WriteLine("myDataReader.GetInt32(1): "+Int32.Parse(myDataReader[1].ToString())+", myDataReader.GetDouble(3): "+Double.Parse(myDataReader[3].ToString()));
						SeriesCollection[Int32.Parse(myDataReader[1].ToString())].Values.Add(Double.Parse(myDataReader[3].ToString()));
					}
			}
			Chart.Series=SeriesCollection;
		}

		public Window2()
		{
			timer.Tick+=OnTimedEvent;
			YFormatter=value => value.ToString("C");
			DataContext=this;
			InitializeComponent();
			OnTimedEvent(null, null);
		}
	}
}
