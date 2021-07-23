using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MP_Role_Permission> RolePermission { get; set; }
        public virtual ICollection<MP_AccountRole> AccountRole { get; set; }
    }
}
