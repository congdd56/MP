using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.CRM
{
    public class VOC_SearchTicket
    {
        [Key]
        public long STT { get; set; }
        /// <summary>
        /// Mã sự vụ
        /// </summary>
        public string TicketId { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string RetailCMName { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string NumSeqRetailCM { get; set; }
        /// <summary>
        /// Model
        /// </summary>
        public string ProductModel { get; set; }
        /// <summary>
        /// Serial
        /// </summary>
        public string ProductSeri { get; set; }
        /// <summary>
        /// Màu
        /// </summary>
        public string ProductColor { get; set; }
        /// <summary>
        /// Kênh tiếp nhận
        /// </summary>
        public string TicketChannel { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// Nội dung trao đổi với KH
        /// </summary>
        public string ReceiveContent { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public string SEGMENTID_Name { get; set; }
        /// <summary>
        /// Tỉnh thành
        /// </summary>
        public string SUBSEGMENTID_name { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string SALESDISTRICTID_Name { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string STREET { get; set; }
        /// <summary>
        /// Hiện trạng xử lý
        /// </summary>
        public string TicketStatus { get; set; }
        /// <summary>
        /// Người Tiếp nhận
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string CreatedDateStr { get; set; }
        /// <summary>
        /// Nhân viên chăm sóc
        /// </summary>
        public string TakeCareBy { get; set; }
        /// <summary>
        /// Ngày Bắt đầu CS
        /// </summary>
        public string CreatedDateCSStr { get; set; }
        /// <summary>
        /// Hoàn thành CS
        /// </summary>
        public string CompletedDateStr { get; set; }
        /// <summary>
        /// Tổng thời gian CS
        /// </summary>
        public string SumTotalCS { get; set; }
        /// <summary>
        /// Hướng xử lý sau CS
        /// </summary>
        public string Solution { get; set; }
        /// <summary>
        /// Hiện trạng
        /// </summary>
        public string CurrentStatus { get; set; }
        /// <summary>
        /// Các bước chăm sóc
        /// </summary>
        public string Steeps { get; set; }
        /// <summary>
        /// Nội dung chăm sóc
        /// </summary>
        public string TakeCareContent { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
    }

}
