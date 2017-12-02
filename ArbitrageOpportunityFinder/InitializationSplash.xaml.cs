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

namespace ArbitrageOpportunityFinder
{
    /// <summary>
    /// Interaction logic for InitializationSplash.xaml
    /// </summary>
    public partial class InitializationSplash : Window
    {
        public InitializationSplash()
        {
            InitializeComponent();

            
        }

        public void OnInit(object sender, RoutedEventArgs e)
        {
            DataController.GetInitData();
            DataController.GetTransactionData();

            GlobalData db = GlobalData.Instance;
            db.GenerateTriArbTransactionList();

            this.Close();
        }
    }
}
