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

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for SellPage.xaml
    /// </summary>
    public partial class SellPage : Page
    {
        public SellPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

			if (!(Int32.TryParse(CommodityField.Text, out int Commodity)))
				MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
			if (!(Int32.TryParse(PriceField.Text, out int Price)))
                MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!(Int32.TryParse(AmountField.Text, out int Amount)))
                MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (Commodity > 0 && Price > 0 && Amount > 0)
            {
                BuySellObject myObject = new BuySellObject(Operation.Buy, Commodity, Price, Amount);
                //Send the object here..
            }
            else
            {
                MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
