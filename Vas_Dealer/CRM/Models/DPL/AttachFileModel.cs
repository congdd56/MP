using System;

namespace VAS.Dealer.Models.DPL
{
    public class AttachFileModel
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
    }
}
