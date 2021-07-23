using VAS.Dealer.Models.CRM;
using VAS.Dealer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace VAS.Dealer.Authentication
{
    /// <summary>
    /// Check permittion follow by:
    /// User and Role  --> OR
    /// Permission , [Object|Operation]   --> AND
    /// Object|Operation is couple define a Permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MPAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Permission { get; set; }
        private string[] Permissions
        {
            get => string.IsNullOrEmpty(Permission) ? new string[] { } : Permission.Split(',');
        }

        public new virtual string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.User == null || filterContext.HttpContext.User.Identity.IsAuthenticated == false)
                {

                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                //var _MemoryServices = filterContext.HttpContext.RequestServices.GetService(typeof(IMemoryServices));

                //if (!_MemoryServices.CheckOnline(UserLogon.UserName))
                //{
                //    filterContext.HttpContext.SignOutAsync();
                //    return Redirect("/");
                //}

                //Lấy quyền ra theo cliams ở đây
                var item = JsonConvert.DeserializeObject<UserLogonModel>(((ClaimsIdentity)filterContext.HttpContext.User.Identity).FindFirst("UserData").Value);
                //Check quyền, role
                if (Permissions != null && Permissions.Length > 0)
                {
                    if (!item.Permissions.Intersect(Permissions).Any())
                    {
                        filterContext.Result = new UnauthorizedResult();
                        return;
                    }
                }
                //-------------------------
            }
            catch (Exception)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

        }
    }

    /// <summary>
    /// Xử lý kiểm tra tài khoản có đang đăng nhập không
    /// </summary>
    public class EnsureUserLoggedIn : ActionFilterAttribute
    {
        private readonly IMemoryServices _MemoryServices;

        public EnsureUserLoggedIn()
        {
            // I was unable able to remove the default ctor 
            // because of compilation error while using the 
            // attribute in my controller
        }

        public EnsureUserLoggedIn(IMemoryServices MemoryServices)
        {
            _MemoryServices = MemoryServices;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.User.Identity.Name;
            // Problem: _sessionService is null here
            if (!_MemoryServices.CheckOnline(username))
            {
                context.HttpContext.SignOutAsync();
                var controller = (MPBaseController)context.Controller;
                context.Result = controller.RedirectToAction("login", "account", new { returnUrl = context.HttpContext.Request.Path }); ;
            }
        }
    }
}
