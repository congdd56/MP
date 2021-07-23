using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MP.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class ManagerController : MPBaseController
    {

        #region PROPERTIES
        private readonly ILogger<ManagerController> _logger;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _RoleServices;
        private readonly ICategoryServices _CategoryServices;
        private readonly IMemoryServices _MemoryServices;
        private readonly IConfiguration _Configuration;

        public ManagerController(ILogger<ManagerController> logger, IUserServices userServices,
             IRoleServices RoleServices, ICategoryServices CategoryServices,
             IMemoryServices MemoryServices, IConfiguration Configuration)
        {
            _logger = logger;
            _userServices = userServices;
            _RoleServices = RoleServices;
            _CategoryServices = CategoryServices;
            _MemoryServices = MemoryServices;
            _Configuration = Configuration;
        }

        #endregion

        #region 1. Danh mục
        /// <summary>
        /// Màn hình tài khoản, quyền, danh mục...
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<AccountModel> result = _userServices.GetListAccount(string.Empty);
            var lstRole = JsonConvert.DeserializeObject<List<RoleModel>>(JsonConvert.SerializeObject(_RoleServices.GetListRole(string.Empty)));
            ViewBag.lstRole = lstRole;
            return View(result);
        }



        /// <summary>
        /// Lấy cây thư mục cho danh mục
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Manager/Category/GetTree")]
        public IActionResult GetTree()
        {
            try
            {
                return Ok(_CategoryServices.GetTree());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách category theo catTypeId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Manager/Category/GetCategoryByCatType/{catType}")]
        public IActionResult GetCategoryByCatTypeId(int catType)
        {
            try
            {
                return Ok(_CategoryServices.GetCategoryByCatType(catType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Manager/Category/GetCategoryByCode/{code}")]
        public IActionResult GetCategoryByCode(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) return BadRequest(MPHelper.Conflict);
                return Ok(_CategoryServices.GetCategoryByCode(code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Manager/Category/Update")]
        public IActionResult UpdateCategory(MP_Category model)
        {
            try
            {
                if (model.Id == 0) return BadRequest(MPHelper.Conflict);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Code))
                    return BadRequest("Vui lòng nhập đầy đủ thông tin.");

                _CategoryServices.Update(model, UserLogon.UserName);

                return Ok(new { value = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Thêm mới danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Manager/Category/Insert")]
        public IActionResult InsertCategory(MP_Category model)
        {
            try
            {
                if (model.CatTypeId == 0) return BadRequest(MPHelper.Conflict);
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Code))
                    return BadRequest("Vui lòng nhập đầy đủ thông tin.");
                if (_CategoryServices.CheckExistCategoryCode(model.Code))
                    return BadRequest("Mã danh mục đã tồn tại.");

                _CategoryServices.Add(model, UserLogon.UserName);

                return Ok(new { value = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Manager/Category/AddCatType")]
        public IActionResult AddCatType(MP_CatType model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name)) return BadRequest("Vui lòng nhập đầy đủ thông tin.");

                _CategoryServices.AddCatType(model);

                return Ok(new { value = "Tạo mới thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa danh mục
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Manager/Category/Delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _CategoryServices.DeleteCategory(id);

                return Ok(new { value = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region 4. Cấu hình email
        public IActionResult MailConfig()
        {
            MailConfigModel model = _CategoryServices.GetEmailConfigInUse() ?? new MailConfigModel();
            return View(model);
        }
        /// <summary>
        /// Lưu thông tin cấu hình email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("manager/SaveEmailConfig")]
        [MPAuthorize(Permission = MPConst.view_mail_config)]
        [HttpPost]
        public IActionResult SaveEmailConfig([FromBody] MailConfigModel model)
        {
            #region VALIDATE
            if (!ModelState.IsValid)
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            }

            if (!MPHelper.IsValidEmail(model.UserName))
            {
                return BadRequest("Sai định dạng tài khoản email");
            }
            #endregion


            if (!_CategoryServices.SaveMailConfig(model, UserLogon.UserName, out string message))
            {
                return BadRequest(message);
            }
            return Ok(new { value = "OK" });

        }

        /// <summary>
        /// Lưu nội dung của email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("manager/SaveEmailContent")]
        [MPAuthorize(Permission = MPConst.view_mail_config)]
        [HttpPost]
        public IActionResult SaveEmailContent([FromBody] MailConfigModel model)
        {
            #region VALIDATE
            if (!_CategoryServices.CheckExistEmailConfig(model.Id))
            {
                return BadRequest("Không tìm thấy thông tin cấu hình email");
            }

            if (string.IsNullOrEmpty(model.MailContent))
            {
                return BadRequest("Nội dung mail không được để chống");
            }
            #endregion

            if (!_CategoryServices.SaveMailContent(model, UserLogon.UserName, out string message))
            {
                return BadRequest(message);
            }
            return Ok(new { value = "OK" });

        }
        #endregion

        #region 6. Tài khoản đăng nhập
        public IActionResult UserOnline()
        {
            return View(_MemoryServices.GetUserOnline());
        }
        #endregion
    }
}