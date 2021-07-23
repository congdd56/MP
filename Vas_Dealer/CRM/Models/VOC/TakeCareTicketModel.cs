using MP.Common;
using System;

namespace VAS.Dealer.Models.VOC
{
    public class TakeCareTicketModel
    {
        public string TicketId { get; set; }
        public string TakeCareBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate == DateTime.MinValue ? string.Empty : CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public DateTime? CompletedDate { get; set; }
        public string CompletedDateStr { get => CompletedDate.HasValue ? CompletedDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string Solution { get; set; }
        public string CurrentStatus { get; set; }
        public string Steeps { get; set; }
        public string TakeCareContent { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public int TransferBy { get; set; }
        public bool IsClosed { get; set; }
        public string ClosedBy { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ClosedDateStr { get => ClosedDate.HasValue ? ClosedDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
    }
}
