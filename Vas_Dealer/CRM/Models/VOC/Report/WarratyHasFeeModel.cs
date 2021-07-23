using MP.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC.Report
{
    public class WarrantyHasFeeIndexModel
    {
        public SelectList CurrentTicketStatus { get; set; }
        public SelectList ProductStatus { get; set; }
    }

    public class WarrantyHasFeeRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CurrentTicketStatus { get; set; }
        public string ProductStatus { get; set; }
        /// <summary>
        /// Thời gian chọn có chứng từ và case được chuyển đi
        /// </summary>
        public string DocFromDate { get; set; }
        public string DocToDate { get; set; }
    }

    public class WarrantyHasFeeResponseModel
    {
        public int STT { get; set; }
        public string TicketId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerDistrict { get; set; }
        public string CustomerProvince { get; set; }
        public string CustomerArea { get; set; }
        public string CustomerPhone { get; set; }
        public string StoreName { get; set; }
        public string ProductStatus { get; set; }
        /// <summary>
        /// Mã tình trạng hư hỏng
        /// </summary>
        public string CrashStatus { get; set; }
        /// <summary>
        /// Hiện trạng xử lý
        /// </summary>
        public string CurrentTicketStatus { get; set; }
        public string CrashStatusDescription { get; set; }
        public string Item { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        public string Serial2 { get; set; }
        public string DocType { get; set; }
        public string Invoice { get; set; }
        public string PurchaseDate { get; set; }
        public string ProductInvoiceDate { get; set; }
        /// <summary>
        /// Nếu tìm model, serial trong GetMPLoadEWarrantyView:     
        /// 1. Có dữ liệu, nhưng không đủ điều kiện bảo hành => không đủ điều kiện bảo hành
        /// 2. Không có dữ liệu: => No data
        /// </summary>
        public string Note { get; set; }
        public string MPLoadElSerial { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public string NullData { get => string.Empty; }
    }

    public class WarrantyHasFeeExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<WarrantyHasFeeResponseModel> Details { get; set; }
    }

}
