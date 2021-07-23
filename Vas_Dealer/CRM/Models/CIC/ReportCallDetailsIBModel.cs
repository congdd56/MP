using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class ReportCallDetailsIBModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> Agent { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}
