using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_CatType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MP_Category> Categories { get; set; }
    }
}
