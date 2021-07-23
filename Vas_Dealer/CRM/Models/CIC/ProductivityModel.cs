using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using VAS.Dealer.Models.Entities.CIC.Store;

namespace VAS.Dealer.Models.CIC
{
    public class ProductivityRequestModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<string> Agent { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PhoneNumber { get; set; }

    }


    public class ProductivityIndexModel
    {
        public SelectList TimeLine { get; set; }
        public SelectList Agent { get; set; }
    }

    public class ProductivityExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_Agent_Productivity> ListItem { get; set; }
    }
    public class IBProductivityExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_IB_Productivity> ListItem { get; set; }
    }
}
