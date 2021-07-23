using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int GroupId { get; set; }

        public virtual ICollection<MP_Role_Permission> RolePermission { get; set; }
        public virtual MP_GroupPermission MP_GroupPermission { get; set; }
    }
}
