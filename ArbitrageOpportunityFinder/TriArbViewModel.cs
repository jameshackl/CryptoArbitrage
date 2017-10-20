using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriArbTool
{
    public class TriArbViewModel
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
}
