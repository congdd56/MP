using MP.Common;
using VAS.Dealer.Models.DPL;
using VAS.Dealer.Models.VOC;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class VOCReceiveTicketModel
    {
        public int STT { get; set; }
        public string DT_RowId { get => TicketId; }
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
        public string ProductExpiredDateStr { get; set; }
        public DateTime? ProductExpiredDate { get; set; }
        public string ProductInvoiceDateStr { get; set; }
        public DateTime? ProductInvoiceDate { get; set; }
        public bool ProductHadDocument { get; set; }
        public string ProductPurchaseCheckContent { get; set; }
        public string ProductPurchaseDateStr { get; set; }
        public DateTime? ProductPurchaseDate { get; set; }
        public string ProductDocType { get; set; }
        public string ProductOtherDocType { get; set; }
        public string ProductDocNum { get; set; }
        /// <summary>
        /// Tình trạng hàng
        /// </summary>
        public string ProductStatus { get; set; }
        /// <summary>
        /// Hiện trạng xử lý
        /// </summary>
        public string CurrentTicketStatus { get; set; }
        /// <summary>
        /// Tình trạng hư hỏng
        /// </summary>
        public string ProductCrashStatus { get; set; }
        /// <summary>
        /// Nội dung trao đổi với KH
        /// </summary>
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
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate == DateTime.MinValue ? string.Empty : CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public string CreatedBy { get; set; }

        public DPLCustomerModel Customer { get; set; } = new DPLCustomerModel();
        public DPLCustomerModel Store { get; set; } = new DPLCustomerModel();

        public DiscusTicketModel DiscusTicket { get; set; }
        public TechTicketModel TechTicket { get; set; }
        public StationTicketModel StationTicket { get; set; }
        public TakeCareTicketModel TakeCareTicket { get; set; }
        public string MPLoadElSerial { get; set; }


        /// <summary>
        /// Đính kèm file hóa đơn
        /// </summary>
        public AttachFileModel AttachFileInvoice { get; set; }
        /// <summary>
        /// Đính kèm file mẫu sản phẩm
        /// </summary>
        public AttachFileModel AttachFileTemp { get; set; }
        /// <summary>
        /// Đính kèm file tình trạng hàng hóa
        /// </summary>
        public AttachFileModel AttachFileTTHH { get; set; }

        public TakeCareTicketModel TakeCare { get; set; }

    }


    public class SearchTicketExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<VOC_SearchTicket> ListItem { get; set; }
    }
    public class TicketExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<VOC_ExportTicket> ListItem { get; set; }
    }
}
