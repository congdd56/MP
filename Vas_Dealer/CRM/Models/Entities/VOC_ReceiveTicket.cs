using VAS.Dealer.Models.Entities.DPL;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class VOC_ReceiveTicket
    {
        public string TicketId { get; set; }
        public string CallerPhone { get; set; }
        public string CallerEmail { get; set; }
        public string TicketChannel { get; set; }
        public string TicketSource { get; set; }
        public string TicketPurpose { get; set; }
        public string TicketArea { get; set; }
        public string TicketProvince { get; set; }
        public string TicketDistrict { get; set; }
        public string TicketAddress { get; set; }
        public string ConfirmCustomerCode { get; set; }
        public Guid? ConfirmCustomerCodeId { get; set; }
        public string ConfirmStoreCode { get; set; }
        public Guid? ConfirmStoreCodeId { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
        public string ProductModel { get; set; }
        public string ProductModel2 { get; set; }
        public string ProductSeri2 { get; set; }
        public string ProductCode { get; set; }
        public string ProductCode2 { get; set; }
        public string ProductColor { get; set; }
        public string ProductCapacity { get; set; }
        public string ProductSeri { get; set; }
        public string ProductSeriCheckContext { get; set; }
        public DateTime? ProductExpiredDate { get; set; }
        public bool ProductHadDocument { get; set; }
        public DateTime? ProductDocumentCompleteDate { get; set; }
        public string ProductPurchaseCheckContent { get; set; }
        public DateTime? ProductPurchaseDate { get; set; }
        public DateTime? ProductInvoiceDate { get; set; }
        public string ProductDocType { get; set; }
        public string ProductOtherDocType { get; set; }
        public string ProductDocNum { get; set; }
        public string ProductStatus { get; set; }
        public string CurrentTicketStatus { get; set; }
        public string ProductCrashStatus { get; set; }
        public string ReceiveContent { get; set; }
        public string TicketAssign { get; set; }
        public DateTime? TicketAssignDate { get; set; }
        public string TicketAttach { get; set; }
        public string TicketStatus { get; set; }
        /// <summary>
        /// Đạt điều kiện bảo hành hay không
        /// True: đạt, False: không
        /// </summary>
        public bool IsWarranty { get; set; }
        public string MPLoadElSerial { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        /// <summary>
        /// Danh sách file đính kèm
        /// </summary>
        public virtual ICollection<DPL_AttachFile> AttachFiles { get; set; }
        public virtual DPL_Customer Customer { get; set; }
        //public virtual DPL_Customer Store { get; set; }
        //public virtual ICollection<VOC_ReceiveAssign> ReceiveAssign { get; set; }
        public virtual VOC_DiscusTicket DiscusTicket { get; set; }
        public virtual VOC_TechTicket TechTicket { get; set; }
        public virtual VOC_StationTicket StationTicket { get; set; }
        public virtual VOC_TakeCareTicket TakeCareTicket { get; set; }
    }

    public class VOC_DiscusTicket
    {
        public string TicketId { get; set; }
        public string UserDiscus { get; set; }
        public string UserDiscusTransfer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DiscusBegin { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string DiscusContent { get; set; }
        public string ErrorCategory { get; set; }
        public string Solution { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual VOC_ReceiveTicket ReceiveTicket { get; set; }
    }

    public class VOC_TechTicket
    {
        public string TicketId { get; set; }
        public string TranTeamleader { get; set; }
        public DateTime? TranTeamleaderDate { get; set; }
        public string TranEmployee { get; set; }
        public DateTime? TranEmployeeDate { get; set; }
        public string Solution { get; set; }
        public string Station { get; set; }
        public bool IsRequestDoc { get; set; }
        public bool IsConfirmDoc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual VOC_ReceiveTicket ReceiveTicket { get; set; }
        public virtual List<VOC_RequestCreatedDoc> RequestDocs { get; set; }

    }

    public class VOC_StationTicket
    {
        public string TicketId { get; set; }
        /// <summary>
        /// Thông tin trạm
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// Tình trạng lỗi KT SP
        /// </summary>
        public string ErrorSatus { get; set; }
        /// <summary>
        /// Danh mục mã lỗi
        /// </summary>
        public string ErrorCategory { get; set; }
        public string Solution { get; set; }
        public string Tranfer { get; set; }
        public bool IsCreatedDoc { get; set; }
        public int CreatedDocCounter { get; set; }
        /// <summary>
        /// Số vận đơn rút LK/HH
        /// </summary>
        public string ShippingWithDrawNumber { get; set; }
        /// <summary>
        /// Ngày nhận LK/HH
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// Trạng thái vận đơn rút LKPT
        /// </summary>
        public string WithdrawStatus { get; set; }
        /// <summary>
        /// Số bảng kê SC-KĐ
        /// </summary>
        public string EnumerationNumber { get; set; }
        /// <summary>
        /// Ngày lên bảng kê SC-KĐ
        /// </summary>
        public DateTime? EnumerationDate { get; set; }
        /// <summary>
        /// Ngày hoàn thành
        /// </summary>
        public DateTime? EnumerationCompleteDate { get; set; }
        /// <summary>
        /// Trạng thái xử lý cuối cùng
        /// </summary>
        public string LastHandlerStatus { get; set; }
        /// <summary>
        /// Ngày dự kiến làm xong
        /// </summary>
        public DateTime? HandlerExpectedDate { get; set; }
        /// <summary>
        /// Ngày xử lý xong
        /// </summary>
        public DateTime? HandlerCompleteDate { get; set; }
        /// <summary>
        /// Quá trình xử lý xong
        /// </summary>
        public string Process { get; set; }
        /// <summary>
        /// Tháng trạm làm báo cáo
        /// </summary>
        public DateTime? ReportMonth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual VOC_ReceiveTicket ReceiveTicket { get; set; }
    }

    public class VOC_TakeCareTicket
    {
        public string TicketId { get; set; }
        public string TakeCareBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
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
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual VOC_ReceiveTicket ReceiveTicket { get; set; }

    }
}
