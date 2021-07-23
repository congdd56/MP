using System;

namespace VAS.Dealer.Models.DPL
{
    public class DPLCustomerModel
    {
        public int STT { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string NumSeqRetailCM { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// Mã khu vực
        /// </summary>
        public string SEGMENTID { get; set; }
        /// <summary>
        /// Tên khu vực
        /// </summary>
        public string SEGMENTID_Name { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string STREET { get; set; }
        /// <summary>
        /// Mã tỉnh
        /// </summary>
        public string SUBSEGMENTID { get; set; }
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        public string SUBSEGMENTID_name { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string RetailCMName { get; set; }
        /// <summary>
        /// Mã huyện
        /// </summary>
        public string SALESDISTRICTID { get; set; }
        /// <summary>
        /// Tên huyện
        /// </summary>
        public string SALESDISTRICTID_Name { get; set; }
        /// <summary>
        /// Key lưu phía MP
        /// </summary>
        public int? SALESDISTRICTID_Key { get; set; }
        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public string RetailCMTyple { get; set; }
        public string RetailCMTyple_Des { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }
        public string StatusNew { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class DPLCustomerSearchModel
    {
        public int draw { get; set; }
        public DPLCustomerSearchDetailModel search { get; set; }
    }

    public class DPLCustomerSearchDetailModel
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
}
