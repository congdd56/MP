using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MP.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Provider;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Controllers
{
    public class AccountController : MPController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IConfiguration _Configuration;
        private readonly IHubContext<CCEvents> _CCEvents;
        private readonly IMemoryServices _MemoryServices;
        private readonly ICategoryServices _CategoryServices;


        public AccountController(ILogger<AccountController> logger, IUserServices userService,
            IRoleServices roleServices, IMemoryServices memoryServices, ICategoryServices CategoryServices,
            IConfiguration Configuration, IHubContext<CCEvents> CCEvents)
        {
            _logger = logger;
            _userServices = userService;
            _roleServices = roleServices;
            _Configuration = Configuration;
            _CCEvents = CCEvents;
            _MemoryServices = memoryServices;
            _CategoryServices = CategoryServices;

        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LogOut()
        {
            _userServices.RemoveAccountOnline(UserLogon.UserName);
            _userServices.AccountLog(UserLogon.UserName, AccountLoginTime.Logout);
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
        /// <summary>
        /// Thông tin tài khoản hiện tại
        /// </summary>
        /// <returns></returns>
        public ActionResult Info()
        {
            #region Tài khoản online
            if (!_MemoryServices.CheckOnline(UserLogon.UserName))
            {
                HttpContext.SignOutAsync();
                return Redirect("/");
            }
            #endregion

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var currentUser = JsonConvert.DeserializeObject<UserLogonModel>(claimsIdentity.FindFirst("UserData").Value);

            AccountModel account = _userServices.GetAccountById(currentUser.UserId);
            return View(account);
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public object AccountSetting(AccountModel obj)
        {
            #region Tài khoản online
            if (!_MemoryServices.CheckOnline(UserLogon.UserName))
            {
                HttpContext.SignOutAsync();
                return Redirect("/");
            }
            #endregion

            var result = _userServices.UpdateInfoAccount(obj);
            return new { value = result };
        }

        #region << Login Actions >>
        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model: model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl = null)
        {
            try
            {
                #region Xử lý Claims
                var user = _userServices.GetLogonUser(model.UserName, model.Password, out string message);
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Message = message;
                    return View(model);
                }

                var claims = new List<Claim>{
                    new Claim("user", model.UserName), 
                    new Claim("UserData", JsonConvert.SerializeObject(user)), 
                    new Claim("role", JsonConvert.SerializeObject(user.RoleCodes)),
                    new Claim("token", user.TokenData.Accesstoken.ToString()),
                    new Claim("permission", JsonConvert.SerializeObject(user.Permissions)),
                    new Claim("PermissionReceive", JsonConvert.SerializeObject(user.PermissionReceive))
                    };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")),
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.Now.AddHours(double.Parse(_Configuration.GetSection("UserTokenSetting:Expires").Value)),
                            IsPersistent = true
                        });
                #endregion

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                ViewData["Error"] = ex;
                return View(model);
            }
        }


        public IActionResult AccessDenied(string returnUrl = null)
        {
            if (returnUrl is null)
            {
                throw new ArgumentNullException(nameof(returnUrl));
            }

            return View();
        }


        [HttpPost]
        public object GetListAccount(string UserName)
        {
            var result = _userServices.GetListAccount(UserName);
            return new { value = result };
        }

        /// <summary>
        /// Tạo mới tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/Account/AddAccount")]
        public IActionResult AddAccount([FromBody] AccountModel obj)
        {
            var result = _userServices.AddAccount(obj, UserLogon.UserName);
            return Ok(new { value = result });
        }
        [HttpPost]
        public object UpdateAccount([FromBody] AccountModel obj)
        {
            var result = _userServices.UpdateAccount(obj);
            return new { value = result };
        }
        [HttpPost]
        public IActionResult ResetPassword(int Id)
        {
            if (_userServices.ResetPassword(Id, out string pw))
            {
                return Ok(new { value = pw });
            }
            return BadRequest(new { value = "Không tìm thấy thông tin tài khoản" });
        }
        [HttpPost]
        public object ChangePassword([FromBody] ChangePasswordModel model)
        {
            var result = _userServices.ChangePassword(model.Id, model.passwordOld, model.passwordNew);
            return new { value = result };
        }
        [HttpPost]
        public object DeleteAccount(int Id)
        {
            var result = _userServices.DeleteAccount(Id, UserLogon.UserName);
            return new { value = result };
        }
        [HttpPost]
        public object UnLockAccount(int Id, bool IsLock)
        {
            var result = _userServices.UnLockAccount(Id, IsLock);
            return new { value = result };
        }
        [HttpPost]
        public object GetInfoAccountById(int Id)
        {
            var result = _userServices.GetInfoAccountById(Id);
            return new { value = result };
        }
        [HttpGet]
        public object GetAllRole()
        {
            var result = _roleServices.GetAllRole();
            return new { value = result };
        }
        #endregion


        #region Quên mật khẩu
        public ActionResult ForgotPw()
        {
            return View();
        }
        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Account/GetCode/{userName}")]
        public IActionResult GetCode(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest("Vui lòng nhập tài khoản");

            //Kiểm tra mail người dùng
            if (!_userServices.CheckExistUserByUserName(userName))
            {
                return BadRequest("Tài khoản không tồn tại hoặc bị khóa.");
            }

            if (!_userServices.GetEmailByUserName(userName, out string message))
            {
                return BadRequest("Tài khoản không tồn tại email hoặc sai định dạng.");
            }


            //Kiểm tra cấu hình mail
            MailConfigModel conf = _CategoryServices.GetEmailConfigInUse();
            if (conf == null)
            {
                return BadRequest("Chưa cấu hình gửi email trên hệ thống. Vui lòng liên hệ quản trị.");
            }

            if (!_userServices.GetCodeRecover(userName, conf, message, out RecoverPasswordModel msg))
            {
                return BadRequest(new { value = msg });
            }
            return Ok(new { value = msg });
        }

        /// <summary>
        /// Rest mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="code"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Account/Recoverypw/{userName}/{code}/{NewPassword}")]
        public IActionResult RecoveryPassword(string userName, string code, string NewPassword)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest("Đừng yêu em nữa");
            if (string.IsNullOrEmpty(NewPassword)) return BadRequest("Đừng yêu em nữa");

            if (!_userServices.RecoveryPassword(userName, code, NewPassword, out string msg))
            {
                return BadRequest(new { value = msg });
            }
            return Ok(new { value = "OK" });
        }
        #endregion
    }
}
