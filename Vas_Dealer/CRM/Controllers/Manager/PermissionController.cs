using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CC;
using VAS.Dealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class PermissionController : MPBaseController
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionServices _PermissionServices;
        private readonly IRoleServices _RoleServices;
        private readonly IUserServices _UserServices;

        public PermissionController(ILogger<PermissionController> logger, IPermissionServices PermissionService, IUserServices UserServices, IRoleServices RoleServices)
        {
            _logger = logger;
            _PermissionServices = PermissionService;
            _UserServices = UserServices;
            _RoleServices = RoleServices;

        }
        [HttpPost]
        public object GetListPermission(string Name)
        {
            var result = _PermissionServices.GetListPermission(Name);
            return new { value = result };
        }


        public IActionResult ListGroupPermission()
        {
            return View();
        }

        /// <summary>
        /// lấy dang sách chức năng
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Permission/ListGroupPermission")]

        public IActionResult ListPermission()
        {
            return Ok(_PermissionServices.GetGroupPermission());
        }

        /// <summary>
        /// Lấy Nhóm chức năng theo ID
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Permission/ListGroupPermissionByRole/{RoleId}")]
        public IActionResult ListGroupPermissionByRole(int RoleId)
        {
            return Ok(_PermissionServices.ListGroupPermissionByRole(RoleId));
        }

        /// <summary>
        /// UpdateRole cập nhật quyền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Permission/UpdateRole")]
        public IActionResult UpdateRole([FromBody] RolePerModel model)
        {
            return Ok(_PermissionServices.UpdateRole(model, UserLogon.UserName));
        }
        /// <summary>
        /// AddRole Tạo mới quyền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Permission/AddRole")]
        public IActionResult AddRole([FromBody] RolePerModel model)
        {
            return Ok(_PermissionServices.AddRole(model, UserLogon.UserName));
        }

    }
}
