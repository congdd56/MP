using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC.Report
{
    public class SourceRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Area { get; set; }
    }
    public class SourceResponseModel
    {
        public int STT { get; set; }
        public string TicketArea { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// số lượng đại lý
        /// </summary>
        public double TicketSourceAgency { get; set; }
        /// <summary>
        /// Số lượng siêu thị
        /// </summary>
        public double TicketSourceSupermarket { get; set; }

        /// <summary>
        /// Số lượng người tiêu dùng
        /// </summary>
        public double TicketSourceConsumers { get; set; }
      
        /// <summary>
        /// Tổng cộng
        /// </summary>
        public double? SumSource { get; set; }
    }
    public class SourceExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<SourceResponseModel> Details { get; set; }
    }


}
