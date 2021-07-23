namespace VAS.Dealer.Models.DPL
{
    public class CheckMPLoadElSerialModel
    {
        public string Item { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }

        public bool Found { get; set; }
        public bool RequiredDoc { get; set; }
        public bool RequestDoc { get; set; }
        public bool IsExpired { get; set; }
        public string PurchaseDate { get; set; }
        public string ExpiredDate { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Trường hợp tìm thấy ở MPLoadEWarrantyView
        /// </summary>
        public bool IsFound_MPLoadEWarrantyView { get; set; }
        /// <summary>
        /// Sử dụng cho báo cáo CA bảo hành tính phí
        /// </summary>
        public string INVOICEDATE { get; set; }
    }

    public class CheckMPLoadEWarrantyViewRequestModel
    {
        public string Model { get; set; }
        public string Item { get; set; }
        /// <summary>
        /// Loại nghành hàng
        /// </summary>
        public string ItemGroup { get; set; }
        public string Serial { get; set; }
        /// <summary>
        /// Thời gian mua hàng, người dùng nhập
        /// </summary>
        public string DocDate { get; set; }
        /// <summary>
        /// Thời gian mua hàng, lấy từ service trước
        /// </summary>
        public string InvoiceDate { get; set; }
    }

}
