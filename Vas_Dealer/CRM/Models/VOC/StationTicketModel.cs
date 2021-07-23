using System;

namespace VAS.Dealer.Models.VOC
{
    public class StationTicketModel
    {
        public string TicketId { get; set; }
        public string Station { get; set; }
        public string Tranfer { get; set; }
        public string Solution { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public StationTab2Model Tab2 { get; set; }
        public StationTab3Model Tab3 { get; set; }
        public StationTab4Model Tab4 { get; set; }
    }

    /// <summary>
    /// Chuyển-nhận linh kiện hàng
    /// </summary>
    public class StationTab2Model
    {
        public string TicketId { get; set; }
        /// <summary>
        /// Số vận đơn rút LK/HH
        /// </summary>
        public string ShippingWithDrawNumber { get; set; }
        /// <summary>
        /// Ngày nhận LK/HH
        /// </summary>
        public string ReceiveDateStr { get; set; }
        /// <summary>
        /// Trạng thái vận đơn rút LKPT
        /// </summary>
        public string WithdrawStatus { get; set; }
    }

    public class StationTab3Model
    {
        public string TicketId { get; set; }
        /// <summary>
        /// Số bảng kê SC-KĐ
        /// </summary>
        public string EnumerationNumber { get; set; }
        /// <summary>
        /// Ngày lên bảng kê SC-KĐ
        /// </summary>
        public string EnumerationDate { get; set; }
        /// <summary>
        /// Ngày hoàn thành
        /// </summary>
        public string EnumerationCompleteDate { get; set; }
    }

    /// <summary>
    /// Tab xử lý cuối cùng
    /// </summary>
    public class StationTab4Model
    {
        public string TicketId { get; set; }
        /// <summary>
        /// Trạng thái xử lý cuối cùng
        /// </summary>
        public string LastHandlerStatus { get; set; }
        /// <summary>
        /// Ngày dự kiến làm xong
        /// </summary>
        public string HandlerExpectedDate { get; set; }
        /// <summary>
        /// Ngày xử lý xong
        /// </summary>
        public string HandlerCompleteDate { get; set; }
        /// <summary>
        /// Quá trình xử lý xong
        /// </summary>
        public string Process { get; set; }
        /// <summary>
        /// Tháng trạm làm báo cáo
        /// </summary>
        public string ReportMonth { get; set; }
    }
}
