using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities.CIC
{
    public class CC_Group
    {
        public int GroupId { get; set; }
        public string NameGroup { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CC_Account_Group> CC_Account_Group { get; set; }


    }
}