using VAS.Dealer.Models.Entities.CIC.Store;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class RecordingRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? Length { get; set; }
        public string Agents { get; set; }
        public string Direction { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RecordingExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_2021_GetRecording> ListItem { get; set; }
    }
    
}
