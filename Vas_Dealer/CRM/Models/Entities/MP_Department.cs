using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MP_Account_Department> AccountDepartment { get; set; }

    }
}
