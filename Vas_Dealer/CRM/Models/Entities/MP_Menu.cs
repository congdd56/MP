using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MP_Role_Menu> RoleMenu { get; set; }
    }
}
