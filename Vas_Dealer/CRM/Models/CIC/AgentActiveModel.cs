using VAS.Dealer.Models.Entities.CIC.Store;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class AgentActiveExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_2021_Agent_Active> ListItem { get; set; }
    }
}
