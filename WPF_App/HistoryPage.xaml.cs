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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTier.Loggers;

namespace WPF_App
{
	/// <summary>
	/// Interaction logic for HistoryPage.xaml
	/// </summary>
	public partial class HistoryPage : Page
	{
		public string[][] History { get; set; }
        public HistoryPage()
        {
            InitializeComponent();
			History=HistoryLogger.ReadHistory();
			DataContext=this;
        }
	}
}
