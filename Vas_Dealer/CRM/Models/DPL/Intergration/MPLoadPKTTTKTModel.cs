using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.DPL.Intergration
{
    public class MPLoadPKTTTKTModel
    {
        [Required]
        public string DataId { get; set; }
        [Required]
        public string CaseID { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string NumSeqPKTTTKT { get; set; }
        [Required]
        public string PKTStatus { get; set; }
        [Required]
        public string Status { get; set; }
        /// <summary>
        /// Định dạng dd/MM/yyyy
        /// </summary>
        public string DateClose { get; set; }
        /// <summary>
        /// Định dạng dd/MM/yyyy
        /// </summary>
        public string DateCreate { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }

    public class MPLoadPKTTTKTModel_V2
    {
        [Required]
        public string DataId { get; set; }
        /// <summary>
        /// Định dạng dd/MM/yyyy
        /// </summary>
        public string DateClose { get; set; }
        /// <summary>
        /// Định dạng dd/MM/yyyy
        /// </summary>
        public string DateCreate { get; set; }
    }

    public class MPLoadSCLKPTModel
    {
        [Required]
        public string DataId { get; set; }
        [Required]
        public string CaseID { get; set; }
        [Required]
        public string TTBHPhieuKTTTKT { get; set; }
        [Required]
        public string TTBHPhieuSC { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string SCLKPTStatus { get; set; }
        public string Status { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }


    public class MPLoadDNCLKPTModel
    {
        [Required]
        public string DataId { get; set; }
        [Required]
        public string CaseID { get; set; }
        public string DeliveryStatus { get; set; }
        public string TTBHPhieuKTTTKT { get; set; }
        public string DeliveryNumber { get; set; }
        public string FileName { get; set; }
        [Required]
        public string NumSeqPDNCLKPT { get; set; }
        public string PackingSlipId { get; set; }
        public string SalesId { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Hình thức vận chuyển
        /// </summary>
        public string Transport { get; set; }
        public string DatePTLK { get; set; }
        public string DateTBH { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }

    public class APIStatusCodeModel
    {
        public int Error_Code { get; set; }
        public string Error_Desc { get; set; }
    }
}
