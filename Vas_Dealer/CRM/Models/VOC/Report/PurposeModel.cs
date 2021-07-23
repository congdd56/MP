using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC.Report
{
    public class PurposeRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Area { get; set; }
    }
    public class PurposeResponseModel
    {
        public int STT { get; set; }
        public string TicketArea { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Bảo hành
        /// </summary>
        public double TicketPurposeGuarantee { get; set; }
        /// <summary>
        /// Tư vấn chuyên sâu
        /// </summary>
        public double TicketPurposeIndepthAdvice { get; set; }

        /// <summary>
        /// Hết bảo hành
        /// </summary>
        public double TicketPurposeEndGuarabtee { get; set; }
        /// <summary>
        /// tư vấn
        /// </summary>
        public double TicketPurposeAdvisory { get; set; }
        /// <summary>
        /// Mua LKPT
        /// </summary>
        public double TicketPurposeLKPT { get; set; }
        /// <summary>
        /// Mục đích khác
        /// </summary>
        public virtual double? OtherPurpose { get => SumPurpose - (TicketPurposeGuarantee + TicketPurposeIndepthAdvice + TicketPurposeEndGuarabtee + TicketPurposeAdvisory + TicketPurposeLKPT); }
        /// <summary>
        /// Tổng cộng
        /// </summary>
        public double? SumPurpose { get; set; }
    }
    public class PurposeExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<PurposeResponseModel> Details { get; set; }
    }


}
