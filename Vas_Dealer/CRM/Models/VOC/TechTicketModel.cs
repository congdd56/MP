using MP.Common;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC
{
    public class TechTicketModel
    {
        public string TicketId { get; set; }
        public string TranTeamleader { get; set; }
        public DateTime? TranTeamleaderDate { get; set; }
        public string TranEmployee { get; set; }
        public DateTime? TranEmployeeDate { get; set; }
        public string Solution { get; set; }
        public string Station { get; set; }
        /// <summary>
        /// Lưu lại thông tin đã yêu cầu tạo mới phiếu lần nào chưa
        /// </summary>
        public bool IsRequestDoc { get; set; }
        /// <summary>
        /// Gửi tạo mới phiếu
        /// </summary>
        public bool IsNewRequestDoc { get; set; }

        public bool IsConfirmDoc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public string CreatedBy { get; set; }
        /// <summary>
        /// Giành cho quyền manager khi chọn nhiều trên lưới
        /// </summary>
        public List<string> Tickets { get; set; }
    }
}
