using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.VOC
{
    /// <summary>
    /// Lịch sử sản phẩm
    /// </summary>
    public class VOCTicketProductHistory
    {
        [Key]
        public Int64 STT { get; set; }
        public string TicketId { get; set; }
        public string CustomerCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string Purpose { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
    }
}
