using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MasterDataProduct
    {
        public Guid Id { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupId { get; set; }
        public string ItemId { get; set; }
        public string NameAlias { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string TrackingDimension { get; set; }
        public string UnitId { get; set; }
        public string status { get; set; }
    }
}
