using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder
{
    public class TriArbViewModel
    {
        public TriArbViewModel() { }
        public TriArbViewModel(TriArbOpportunity tao)
        {
            exchange1 = tao.arbitrageTransactionChain[0].baseCurrency.exchange.ToString();
            exchange2 = tao.arbitrageTransactionChain[1].baseCurrency.exchange.ToString();
            exchange3 = tao.arbitrageTransactionChain[2].baseCurrency.exchange.ToString();
            exchange4 = tao.arbitrageTransactionChain[2].quoteCurrency.exchange.ToString();

            currency1 = tao.arbitrageTransactionChain[0].baseCurrency.symbol;
            currency2 = tao.arbitrageTransactionChain[1].baseCurrency.symbol;
            currency3 = tao.arbitrageTransactionChain[2].baseCurrency.symbol;
            currency4 = tao.arbitrageTransactionChain[2].quoteCurrency.symbol;

            arbRate1 = Math.Round(tao.arbitrageTransactionChain[0].takerIndicatorRate,4).ToString();
            arbRate2 = Math.Round(tao.arbitrageTransactionChain[1].takerIndicatorRate, 4).ToString();
            arbRate3 = Math.Round(tao.arbitrageTransactionChain[2].takerIndicatorRate, 4).ToString();

            maxVolume1 = "TODO";
            maxVolume2 = "TODO";
            maxVolume3 = "TODO";

            tradeInstruction1 = "TODO";
            tradeInstruction2 = "TODO";
            tradeInstruction3 = "TODO";

            fee1 = "TODO";
            fee2 = "TODO";
            fee3 = "TODO";

            maxInitialVolume = Math.Round(tao.maxInitialVolume,4).ToString(); //TODO: get exchange granularity
            percentageGain = Math.Round((tao.arbitrageRate - 1)*100,4) + "%";
            volumeGain = "TODO";
        }
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

        private string RoundToSigFigsOrDecimalPlaces(decimal number, int sigFigs)
        {
            int decimalPlaces = 0;
            bool decimalFlag = false;
            foreach (char c in number.ToString())
            {
                if (c == '.')
                    decimalFlag = true;

                if (decimalFlag & c == '0')
                    decimalPlaces++;
            }

            return number.ToString();
        }
    }
}
