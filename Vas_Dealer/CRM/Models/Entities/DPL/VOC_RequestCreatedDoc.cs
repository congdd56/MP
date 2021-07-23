using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class VOC_RequestCreatedDoc
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSuccess { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
        public virtual VOC_TechTicket Ticket { get; set; }
    }
}
