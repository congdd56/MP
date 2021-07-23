using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MasterDataProductVariant
    {
        public Guid Id { get; set; }
        public string DisplayProductNumber { get; set; }
        public string InventColorId { get; set; }
        public string InventSizeId { get; set; }
        public string InventStyleId { get; set; }
        public string ItemId { get; set; }
        public string RetailVariantId { get; set; }
        public string Status { get; set; }
        public string configId { get; set; }
    }
}
