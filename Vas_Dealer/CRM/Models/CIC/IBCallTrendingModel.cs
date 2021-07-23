using VAS.Dealer.Models.Entities.CIC.Store;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class IBCallTrendingRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
    }

    public class IBCallTrendingExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_2021_IBCallTrend> ListItem { get; set; }
    }
}
