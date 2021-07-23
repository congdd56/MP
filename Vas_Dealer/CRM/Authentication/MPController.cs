using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using VAS.Dealer.Models.CRM;

namespace VAS.Dealer.Authentication
{

    public abstract class MPController : Controller
    {
        protected UserLogonModel UserLogon
        {
            get
            {
                return JsonConvert.DeserializeObject<UserLogonModel>(((ClaimsIdentity)User.Identity).FindFirst("UserData").Value);
            }
        }

        /// <summary>
        /// Danh sách Teamleader ở tab kỹ thuật
        /// </summary>
        protected List<PermisTeamleaderModel> PermisTeamleader
        {
            get
            {
                return UserLogon.PermisTeamleader;
            }
        }

        /// <summary>
        /// Danh sách chức năng của tài khoản được phép
        /// </summary>
        protected List<string> Permissions
        {
            get
            {
                return UserLogon.Permissions;
            }
        }
    }

    [ServiceFilter(typeof(EnsureUserLoggedIn))]
    public abstract class MPBaseController : Controller
    {
        protected UserLogonModel UserLogon
        {
            get
            {
                return JsonConvert.DeserializeObject<UserLogonModel>(((ClaimsIdentity)User.Identity).FindFirst("UserData").Value);
            }
        }
        /// <summary>
        /// Danh sách chức năng của tài khoản được phép
        /// </summary>
        protected List<string> Permissions
        {
            get
            {
                return UserLogon.Permissions;
            }
        }
    }
}
