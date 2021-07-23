using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities.CIC
{
    public class VipBlacklistRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
    }
    public class VipBlacklistResponseModel
    {
        public int STT { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// loại danh sách
        /// </summary>
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class VipBlacklistExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<VipBlacklistResponseModel> Details { get; set; }
    }
    public class VIP_BLACK_List
    {
       
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// loại danh sách
        /// </summary>
        public string PhoneType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    
}