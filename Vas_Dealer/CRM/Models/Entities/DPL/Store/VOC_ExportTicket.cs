using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.CRM
{
    public class VOC_ExportTicket
    {
        [Key]
        public long STT { get; set; }
        public string TicketId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }

        public string TicketChannel { get; set; }
        public string TicketSource { get; set; }
        public string TicketPurpose { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string RetailCMName { get; set; }
        public string STREET { get; set; }
        public string SALESDISTRICTID_Name { get; set; }
        public string SUBSEGMENTID_name { get; set; }
        public string SEGMENTID_Name { get; set; }
        public string PHONE { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }
        public string ProductCapacity { get; set; }
        public string ProductSeri { get; set; }
        public string ProductSeri2 { get; set; }
        public string ProductModel2 { get; set; }
        public DateTime? ProductPurchaseDate { get; set; }

        public string ProductPurchaseDateStr { get => ProductPurchaseDate.HasValue ? ProductPurchaseDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? ProductExpiredDate { get; set; }
        public string ProductExpiredDateStr { get => ProductExpiredDate.HasValue ? ProductExpiredDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public bool ProductHadDocument { get; set; }
        public string ProductHadDocumentStr { get => ProductHadDocument == true ? "YES" : "NO"; }

        public string ProductDocType { get; set; }
        public string ProductOtherDocType { get; set; }
        public string ProductDocNum { get; set; }
        public string ProductStatus { get; set; }
        public string ProductCrashStatusCode { get; set; }
        public string ProductCrashStatusDescription { get; set; }
        public string ReceiveContent { get; set; }
        public string TicketStatus { get; set; }
        public string UserDiscus { get; set; }
        public string UserDiscusTransfer { get; set; }
        public DateTime? DiscusBegin { get; set; }
        public string DiscusBeginStr { get => DiscusBegin.HasValue ? DiscusBegin.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? DiscusCompleted { get; set; }
        public string DiscusCompletedStr { get => DiscusCompleted.HasValue ? DiscusCompleted.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string DiscusContent { get; set; }
        public string DiscusErrorCategory { get; set; }
        public string DiscusErrorDescription { get; set; }
        public string DiscusSolution { get; set; }
        public string TakeCareBy { get; set; }
        public DateTime? BeginTakeCare { get; set; }
        public string BeginTakeCareStr { get => BeginTakeCare.HasValue ? BeginTakeCare.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }

        public DateTime? CompletedDate { get; set; }
        public string CompletedDateStr { get => CompletedDate.HasValue ? CompletedDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        /// <summary>
        /// Ngày Bắt đầu CS
        /// </summary>
        public string CreatedDateCSStr { get; set; }
        /// <summary>
        /// Tổng thời gian CS
        /// </summary>
        public string SumTotalCS { get; set; }
        public string CurrentStatus { get; set; }
        public string Solution { get; set; }
        public string Steeps { get; set; }
        public string TakeCareContent { get; set; }
        public string Note { get; set; }
        public string Station { get; set; }
        public string NumSeqPKTTTKT { get; set; }
        public DateTime? TransDateCreatePKTTTKT { get; set; }
        public string TransDateCreatePKTTTKTStr { get => TransDateCreatePKTTTKT.HasValue ? TransDateCreatePKTTTKT.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string NumSeqPDNCLKPT { get; set; }
        public string StatusPKTTTKT { get; set; }
        public string TTBHPhieuSC { get; set; }
        public string DeliveryNumber { get; set; }
        public string Transport { get; set; }
        public DateTime? TransDateReceivedPTLK { get; set; }
        public string TransDateReceivedPTLKStr { get => TransDateReceivedPTLK.HasValue ? TransDateReceivedPTLK.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? TransDateRecivedTBH { get; set; }
        public string TransDateRecivedTBHStr { get => TransDateRecivedTBH.HasValue ? TransDateRecivedTBH.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string ShippingWithDrawNumber { get; set; }
        public DateTime? ReceiveDateStation { get; set; }
        public string ReceiveDateStationStr { get => ReceiveDateStation.HasValue ? ReceiveDateStation.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string WithdrawStatus { get; set; }
        public string EnumerationNumber { get; set; }
        public DateTime? EnumerationDate { get; set; }
        public string EnumerationDateStr { get => EnumerationDate.HasValue ? EnumerationDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? EnumerationCompleteDate { get; set; }
        public string EnumerationCompleteDateStr { get => EnumerationCompleteDate.HasValue ? EnumerationCompleteDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string LastHandlerStatus { get; set; }
        public DateTime? HandlerExpectedDate { get; set; }
        public string HandlerExpectedDateStr { get => HandlerExpectedDate.HasValue ? HandlerExpectedDate.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public string Process { get; set; }
        public DateTime? ReportMonth { get; set; }
        public string ReportMonthStr { get => ReportMonth.HasValue ? ReportMonth.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }

    }

}
