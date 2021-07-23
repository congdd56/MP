using MP.Common;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class ManagerLogModel
    {
        public List<LogRequestCreateCustomerModel> LogCustomer { get; set; }
        public List<LogRequestCreateDocModel> LogDoc { get; set; }
    }

    public class LogRequestCreateCustomerModel
    {
        public int STT { get; set; }
        public string CustomerCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
        public DateTime? CompletedDate { get; set; }
        public string CompletedDateStr { get => CompletedDate.HasValue ? CreatedDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public bool IsSuccess { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
    }

    public class LogRequestCreateDocModel
    {
        public int STT { get; set; }
        public string TicketId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSuccess { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
    }

}
