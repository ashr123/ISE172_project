using DataTier.DataEntries;
using System;
using DataTier;
using DataTier.Loggers;
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
    /// Interaction logic for BuyPage.xaml
    /// </summary>
    public partial class BuyPage : Page
    {
        public BuyPage()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int Commodity;
            int Price;
            int Amount;

            if (!(Int32.TryParse(CommodityField.Text, out Commodity)))
                MessageBox.Show("Invalid Commodity", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!(Int32.TryParse(PriceField.Text, out Price)))
                MessageBox.Show("Invalid Price", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (!(Int32.TryParse(AmountField.Text, out Amount)))
                MessageBox.Show("Invalid Amount", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            if (Commodity > 0 || Price > 0 || Amount > 0)
            {
                //BuySellObject myObject = new BuySellObject(Operation.Buy, Commodity, Price, Amount);
                IMarketClient market = new MarketClientClass();
                MarketBuySell marketBuySell = market.SendBuyRequest(Price, Commodity, Amount);
                if (marketBuySell.Error == null)
                {
                    MessageBox.Show("Sucsess!! Your Buy request has been placed. your id is: " + marketBuySell.Id);
                    market.SendCancelBuySellRequest(marketBuySell.Id);
                    String output = Price+","+Commodity+","+Amount+","+"buy" ;
                    HistoryLogger.WriteHistory(output);
                    
                }
                else
                {
                    MessageBox.Show(marketBuySell.ToString());
                }
            }
            else
            {
                MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
