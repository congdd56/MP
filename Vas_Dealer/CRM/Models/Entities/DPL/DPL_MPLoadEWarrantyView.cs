using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MPLoadEWarrantyView
    {
        public Guid Id { get; set; }
        public string CUSTACCOUNT { get; set; }
        public string DELIVERYNAME { get; set; }
        public string INVENTCOLORID { get; set; }
        public string INVENTLOCATIONID { get; set; }
        public string INVENTSERIALID { get; set; }
        public string INVENTSITEID { get; set; }
        public string INVENTSIZEID { get; set; }
        public string INVENTSTYLEID { get; set; }
        public string INVOICEDATE { get; set; }
        public string ITEMGROUPID { get; set; }
        public string ITEMID { get; set; }
        public string NAMEALIAS { get; set; }
        public string PACKINGSLIPDATE { get; set; }
        public string PACKINGSLIPID { get; set; }
        public string SALESID { get; set; }
        public string WMSLOCATIONID { get; set; }
        public string status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
