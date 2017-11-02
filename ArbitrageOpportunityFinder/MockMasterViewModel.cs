using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder
{
    public class MockMasterViewModel
    {

        public ObservableCollection<TriArbViewModel> Items { get; set; }

        public MockMasterViewModel()
        {
            var item01 = new TriArbViewModel() { exchange1 = "Poloniex", currency1 = "BTC",
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

            Items = new ObservableCollection<TriArbViewModel>()
            {
                item01
            };
        }
    }
}
