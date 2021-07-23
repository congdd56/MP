using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class RoleController : MPBaseController
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleServices _roleServices;
        private readonly IPermissionServices _permissionServices;
        public RoleController(ILogger<RoleController> logger, IRoleServices RoleService, IPermissionServices permissionServices)
        {
            _logger = logger;
            _roleServices = RoleService;
            _permissionServices = permissionServices;
        }
        [HttpPost]
        public object GetListRole(string Name)
        {
            var result = _roleServices.GetListRole(Name);
            return new { value = result };
        }
        [HttpGet]
        public object GetAllPermission()
        {
            var result = _permissionServices.GetAllPermission();
            return new { value = result };
        }

        [HttpPost]
        public object AddRole(RoleModel obj)
        {
            var result = _roleServices.AddRole(obj, UserLogon.UserName);
            return new { value = result };
        }
        [HttpPost]
        public object UpdateRole(RoleModel obj)
        {
            var result = _roleServices.UpdateRole(obj, UserLogon.UserName);
            return new { value = result };
        }

        [HttpPost]
        public object DeleteRole(int Id)
        {
            var result = _roleServices.DeleteRole(Id, UserLogon.UserName);
            return new { value = result };
        }
        [HttpPost]
        public object GetInfoRoleById(int Id)
        {
            var result = _roleServices.GetInfoRoleById(Id);
            return new { value = result };
        }
        [HttpPost]
        public object GetInfoUserByRoleId(int Id)
        {
            var result = _roleServices.GetInfoUserByRoleId(Id);
            return new { value = result };
        }
    }
}
