using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_StoreArea
    {
        public string SEGMENTID { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual ICollection<DPL_StoreProvince> StoreProvinces { get; set; }
        public virtual DPL_Customer Customer { get; set; }
    }

    public class DPL_StoreProvince
    {
        public string SEGMENTID { get; set; }
        public string DESCRIPTION { get; set; }
        public string SUBSEGMENTID { get; set; }
        public string SUBSEGMENTDESCRIPTION { get; set; }
        public virtual DPL_StoreArea StoreArea { get; set; }
        public virtual ICollection<DPL_StoreDistrict> StoreDistricts { get; set; }
        public virtual DPL_Customer Customer { get; set; }
    }

    public class DPL_StoreDistrict
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProvinceCode { get; set; }
        public string Description { get; set; }
        public virtual DPL_StoreProvince StoreProvince { get; set; }
        public virtual DPL_Customer Customer { get; set; }
    }

}
