namespace VAS.Dealer.Models.DPL
{
    public class DPLRequestDataModel
    {
        public string pwd { get; set; }
        public string usname { get; set; }
    }

    public class InsertMPUpPhieuKTTTKTModel
    {
        public string CaseID { get; set; }
        /// <summary>
        /// Sang nói
        /// </summary>
        public string CategoryID { get => "Hitachi"; }
        public string Color { get; set; }
        public string Config { get; set; }
        public string InventSerialId { get; set; }
        public string ItemID { get; set; }
        /// <summary>
        /// Mặc định là ASC, Sang nói thế
        /// </summary>
        public string MPStatus { get => "ASC"; }
        public string Model { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string NumSeqRetailCMStore { get; set; }
        /// <summary>
        /// Tình trạng hàng
        /// </summary>
        public string MerchandiseStatus { get; set; }
        public string CustAcountBHUQ { get; set; }
        /// <summary>
        /// Tình trạng hư hỏng
        /// </summary>
        public string ProductStatus { get; set; }
        public string PurchaseDate { get; set; }
        public string Solution { get; set; }
        public string Status { get => "0"; }
        public string VoucherPurchaseNumb { get; set; }
        public string Voucherpurchase { get; set; }
        //"CaseID":"String content", -
        //"CategoryID":"String content", 					=> Hitachi
        //"Color":"String content",						=> Mầu
        //"Config":"String content",						=> Dung tích/cs
        //"DPLProofOfPurchaseType":"String content",		=> bỏ
        //"InventSerialId":"String content",				=> serial
        //"ItemID":"String content",						=> Mã sản phẩm
        //"MPStatus":"String content",					=> "ASC"
        //"MerchandiseStatus":"String content",			=> Bỏ
        //"Model":"String content",						=> Model
        //"Notes":"String content",						=> Bỏ
        //"NumSeqRetailCM":"String content",				=> Mã khách hàng
        //"NumSeqRetailCMStore":"String content",			=> Mã cửa hàng
        //"ProductStatus":"String content",				=> Tình trạng của sản phẩm => gửi text từ trường: Tình trạng hàng bên tiếp nhận
        //"PurchaseDate":"String content",                => dd/MM/yyyy
        //"Solution":"String content",					=> Hướng xử lý sau tư vấn, tab kỹ thuật
        //"Status":"String content",                      => Request = 0, 
        //"TransDateCreate":"String content",				=> DateTime.Now
        //"VoucherPurchaseNumb":"String content",			=> Số chứng từ
        //"Voucherpurchase":"String content"				=> Loại chứng từ
    }

    public class InsertRetailCMModel
    {
        public string NumSeqRetailCM { get; set; }
        public string PHONE { get; set; }
        public string RetailCMName { get; set; }
        public string RetailCMTyple { get; set; }
        public string SALESDISTRICTID { get; set; }
        public string SEGMENTID { get; set; }
        public string STREET { get; set; }
        public string SUBSEGMENTID { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }
        public string StatusNew { get; set; }
    }
}
