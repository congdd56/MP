using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_Customer
    {
        public Guid Id { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string PHONE { get; set; }
        public string SEGMENTID { get; set; }
        public string STREET { get; set; }
        public string SUBSEGMENTID { get; set; }
        public string RetailCMName { get; set; }
        public string SALESDISTRICTID { get; set; }
        public int? SALESDISTRICTID_Key { get; set; }
        public string RetailCMTyple { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }
        public string StatusNew { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual VOC_ReceiveTicket Ticket { get; set; }

        //public virtual VOC_ReceiveTicket TicketStore { get; set; }

        public virtual DPL_StoreArea StoreArea { get; set; }
        public virtual DPL_StoreProvince StoreProvince { get; set; }
        public virtual DPL_StoreDistrict StoreDistrict { get; set; }

    }

    public class DPL_RequestCustomerAPI
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsSuccess { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
    }
}
