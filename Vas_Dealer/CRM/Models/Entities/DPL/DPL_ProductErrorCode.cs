using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_ProductErrorCode
    {
        public int Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string Category { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
