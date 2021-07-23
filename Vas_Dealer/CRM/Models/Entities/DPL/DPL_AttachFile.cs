using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_AttachFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileNameRandom { get; set; }
        public string TempFolder { get; set; }
        public string FileExtension { get; set; }
        public string FileContentType { get; set; }
        public string UploadType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual VOC_ReceiveTicket Ticket { get; set; }
    }
}
