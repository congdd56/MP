using System.Collections.Generic;

namespace VAS.Dealer.Models.Entities
{
    public class MP_GroupPermission
    {
        public int GroupId { get; set; }
        public string NameGroup { get; set; }
        public virtual ICollection<MP_Permission> MP_Permission { get; set; }

    }
}
