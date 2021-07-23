using MP.Common;
using System;

namespace VAS.Dealer.Models.VOC
{
    public class DiscusTicketModel
    {
        public string TicketId { get; set; }
        public string UserDiscus { get; set; }
        public string UserDiscusTransfer { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public DateTime? DiscusBegin { get; set; }
        public string DiscusBeginStr { get => DiscusBegin.HasValue ? DiscusBegin.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : ""; }
        public DateTime? CompletedDate { get; set; }
        public string CompletedDateStr { get => CompletedDate.HasValue ? CompletedDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : ""; }
        public string DiscusContent { get; set; }
        public string ErrorCategory { get; set; }
        public string Solution { get; set; }
        public string CreatedBy { get; set; }
    }
}
