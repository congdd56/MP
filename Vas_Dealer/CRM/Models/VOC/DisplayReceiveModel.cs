namespace VAS.Dealer.Models.VOC
{
    public class DisplayReceiveModel
    {
        /// <summary>
        /// Khu vực đóng case
        /// </summary>
        public bool CloseArea { get; set; }
        /// <summary>
        /// Khu vực số điện thoại gọi đến, tên khách hàng
        /// </summary>
        public bool CallArea { get; set; }
        /// <summary>
        /// Khu vực nhập yêu cầu 1 - 13
        /// </summary>
        public bool DNO { get; set; }
        /// <summary>
        /// Khu vực RNO xử lý
        /// </summary>
        public bool RNO { get; set; }
    }
}
