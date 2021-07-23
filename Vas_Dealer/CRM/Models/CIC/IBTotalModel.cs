using VAS.Dealer.Models.Entities.CIC.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.CIC
{

    public class IBTotalRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class IBTotalExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_2021_IBTotal> ListItem { get; set; }
    }
}
