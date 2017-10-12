using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;


namespace ArbitrageOpportunityFinder
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button_getExchangeData_Click(object sender, EventArgs e)
        {
            GlobalData db = GlobalData.Instance;

            if(DataController.GetInitData())
            {
                //Tada it worked
                textBox_Messages.Text = "Init data complete success!" + Environment.NewLine;
                textBox_Messages.Text = textBox1.Text +"Transactions: "+ db.GetMasterTransactionCount();
            }
            else
            {
                //it failed in some way
            }
        }

        private void button_triArbOpportunities_Click(object sender, EventArgs e)
        {
            GlobalData db = GlobalData.Instance;
            db.GenerateTriArbTransactionChains(3);
            textBox_Messages.Text = "Number of arbitrage chains: " + db.GetTriArbTransChainCount();

            textBox1.Text = db.GetTransactionChainsOutput();
        }

        private void button_GetTriArbTransactions_Click(object sender, EventArgs e)
        {
            GlobalData db = GlobalData.Instance;
            db.GenerateTriArbTransactionList();
            textBox_Messages.Text = "Number of possible transactions: " + db.GetTriArbTransListCount();
        }

        private void button_UpdateTransactionData_Click(object sender, EventArgs e)
        {
            DataController.GetTransactionData();
            GlobalData db = GlobalData.Instance;

            //huh, turns out it never acually changes anything
            textBox1.Text = db.GetTransactionChainsOutput();
        }

        private void button_QuadArbOpportunities_Click(object sender, EventArgs e)
        {
            GlobalData db = GlobalData.Instance;
            db.GenerateTriArbTransactionChains(4);
            textBox_Messages.Text = "Number of arbitrage chains: " + db.GetTriArbTransChainCount();

            textBox1.Text = db.GetTransactionChainsOutput();
        }
    }
}
