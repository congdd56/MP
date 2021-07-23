using VAS.Dealer.Models.CC;
using System.Collections.Generic;
using VAS.Dealer.Models.CRM;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IRoleServices
    {
        object GetListRole(string Name);
        object AddRole(RoleModel obj, string userLogin);
        object UpdateRole(RoleModel obj, string userLogin);
        object DeleteRole(int Id, string userLogin);
        object GetInfoRoleById(int Id);
        object GetAllRole();
        object GetInfoUserByRoleId(int Id);
    }
}
