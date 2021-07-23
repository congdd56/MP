using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.Entities;
using System.Collections.Generic;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IPermissionServices
    {
        List<MP_Permission> GetListPermission(string Name);
        object GetAllPermission();
        object UpdateRole(RolePerModel obj, string userLogin);
        List<GroupPermissionModel> GetGroupPermission();
        object ListGroupPermissionByRole(int RoleId);
        object AddRole(RolePerModel obj, string userLogin);
    }
}
