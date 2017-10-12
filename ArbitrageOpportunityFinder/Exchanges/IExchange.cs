using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder.Exchanges
{
    interface IExchange
    {
        GlobalData.Exchange exchange { get; set; }
        bool marginTradingAvailable { get; set; }
        Currency marginTradingBase { get; set; }

        decimal makerFee { get; set; }
        decimal takerFee { get; set; }


    }
}
