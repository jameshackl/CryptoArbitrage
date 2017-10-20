using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriArbTool
{
    public class MockItemViewModel
    {
        //Transaction 1
        public string exchange1 { get; set; }
        public string currency1 { get; set; }

        public string arbRate1 { get; set; }
        public string maxVolume1 { get; set; }
        public string tradeInstruction1 { get; set; }
        public string fee1 { get; set; }

        //Transaction 2
        public string exchange2 { get; set; }
        public string currency2 { get; set; }

        public string arbRate2 { get; set; }
        public string maxVolume2 { get; set; }
        public string tradeInstruction2 { get; set; }
        public string fee2 { get; set; }

        //Transaction 3
        public string exchange3 { get; set; }
        public string currency3 { get; set; }

        public string arbRate3 { get; set; }
        public string maxVolume3 { get; set; }
        public string tradeInstruction3 { get; set; }
        public string fee3 { get; set; }

        //Transaction 4
        public string exchange4 { get; set; }
        public string currency4 { get; set; }

        //Total Trade
        public string maxInitialVolume { get; set; }
        public string percentageGain { get; set; }
        public string volumeGain { get; set; }
    }

    public class MockMasterViewModel
    {

        public ObservableCollection<MockItemViewModel> Items { get; set; }

        public MockMasterViewModel()
        {
            var item01 = new MockItemViewModel() { exchange1 = "Poloniex", currency1 = "BTC",
                                                    arbRate1 = "1.5", maxVolume1= "0.5", tradeInstruction1 = "trade this way", fee1="0.001",
                exchange2 = "Poloniex",
                currency2 = "LTC",
                arbRate2 = "1.5",
                maxVolume2 = "0.5",
                tradeInstruction2 = "trade this way",
                fee2 = "0.001",
                exchange3 = "Poloniex",
                currency3 = "ETH",
                arbRate3 = "1.5",
                maxVolume3 = "0.5",
                tradeInstruction3 = "trade this way",
                fee3 = "0.001",
                exchange4 = "Poloniex",
                currency4 = "BTC",
                maxInitialVolume = "0.1",
                percentageGain = "0.5%",
                volumeGain = "0.0001"

            };
            //var item02 = new MockItemViewModel() { Title = "Title 02", Address = "Address 02" };
            Items = new ObservableCollection<MockItemViewModel>()
            {
                item01
            };
        }
    }
}
