using System;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Category
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CatTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SubCatTypeCode { get; set; }
        public int? OrderBy { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public string ExpandProperties { get; set; }
        public virtual MP_CatType CatType { get; set; }
    }
}
