using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MPLoadPKTTTKT
    {
        public Guid Id { get; set; }
        public string DataId { get; set; }
        public string CaseID { get; set; }
        public string FileName { get; set; }
        public string NumSeqPKTTTKT { get; set; }
        public string PKTStatus { get; set; }
        public string Status { get; set; }
        public DateTime? TransDateClose { get; set; }
        public DateTime? TransDateCreate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DPL_MPLoadSCLKPT
    {
        public Guid Id { get; set; }
        public string DataId { get; set; }
        public string CaseID { get; set; }
        public string TTBHPhieuKTTTKT { get; set; }
        public string TTBHPhieuSC { get; set; }
        public string FileName { get; set; }
        public string SCLKPTStatus { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }


    public class DPL_MPLoadDNCLKPT
    {
        public Guid Id { get; set; }
        public string DataId { get; set; }
        public string CaseID { get; set; }
        public string DeliveryStatus { get; set; }
        public string TTBHPhieuKTTTKT { get; set; }
        public string DeliveryNumber { get; set; }
        public string FileName { get; set; }
        public string NumSeqPDNCLKPT { get; set; }
        public string PackingSlipId { get; set; }
        public string SalesId { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Hình thức vận chuyển
        /// </summary>
        public string Transport { get; set; }
        public DateTime? TransDateReceivedPTLK { get; set; }
        public DateTime? TransDateRecivedTBH { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
