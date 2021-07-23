using System.Collections.Generic;

namespace VAS.Dealer.Models.CC
{
    public class GroupPermissionModel
    {
        public string GroupName { get; set; }
        public List<PermissionModel> Permissions { get; set; }
    }
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Permission { get; set; }
        public bool IsCheck { get; set; }


    }
    public class RolePerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
        public string Users { get; set; }

    }

    public class GroupPermissionRoleModel
    {
        public string GroupName { get; set; }
        public List<PermissionModel> Permissions { get; set; }
        public List<RolePermision> RolePermision { get; set; }

    }
    public class RolePermision
    {
        public int RoleId { get; set; }
        public int PerId { get; set; }

    }
}
