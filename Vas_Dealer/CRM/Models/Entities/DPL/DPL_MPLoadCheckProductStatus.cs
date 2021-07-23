using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MPLoadCheckProductStatus
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string InventSizeId { get; set; }
        public string InventStyleId { get; set; }
        public string ItemGroupId { get; set; }
        public string ItemID { get; set; }
        public string Month { get; set; }
        public string SerialNum { get; set; }
        public string Status { get; set; }
        public string configId { get; set; }
    }
}
