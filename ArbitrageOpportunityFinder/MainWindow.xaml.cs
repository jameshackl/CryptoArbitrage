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

namespace ArbitrageOpportunityFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializationSplash inspl = new InitializationSplash();
            inspl.Show();

            InitializeComponent();

        }

        private void button_GetTriArbOpportunities_Click(object sender, RoutedEventArgs e)
        {
            GlobalData db = GlobalData.Instance;

            db.GenerateTriArbTransactionChains(
                comboBox_StartingCurrency.Text,
                (GlobalData.Exchange)Enum.Parse(typeof(GlobalData.Exchange),comboBox_StartingExchange.Text),
                (GlobalData.Exchange)Enum.Parse(typeof(GlobalData.Exchange),comboBox_EndingExchange.Text));

            TriArbMasterViewModel mViewModel = new TriArbMasterViewModel();
            triArbDisplayBox.ItemsSource = mViewModel.Items;

            //label countTriArbOpportunities = count of tri arb opportunities
        }

        private void button_UpdatePricesAndVolumes_Click(object sender, RoutedEventArgs e)
        {
            //update prices and volume orderbooks

        }

    }
}
