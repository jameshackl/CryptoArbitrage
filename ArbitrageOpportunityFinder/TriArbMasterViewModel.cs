using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder
{
    public class TriArbMasterViewModel
    {

        public ObservableCollection<TriArbViewModel> Items { get; set; }

        public TriArbMasterViewModel()
        {
            GlobalData db = GlobalData.Instance;
            Items = db.GetTriArbOutput();
        }
    }
}
