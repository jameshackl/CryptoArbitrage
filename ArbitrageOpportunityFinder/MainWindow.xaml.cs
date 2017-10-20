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

namespace TriArbTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cB_EndingExchange_Selected(object sender, SelectionChangedEventArgs e)
        {
            //set variables in the global variables
        }

        private void cB_StartingCurrency_Selected(object sender, SelectionChangedEventArgs e)
        {
            //set variables in the global variables
        }

        private void cB_StartingExchange_Selected(object sender, SelectionChangedEventArgs e)
        {
            //set variables in the global variables
        }

        private void button_GetTriArbOpportunities_Click(object sender, RoutedEventArgs e)
        {


            //label countTriArbOpportunities = count of tri arb opportunities
        }

        private void button_UpdatePricesAndVolumes_Click(object sender, RoutedEventArgs e)
        {
            //update prices and volume orderbooks
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
