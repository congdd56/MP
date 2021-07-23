using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLogonModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public bool IsSysAdmin { get; set; }
        public int AgentID { get; set; }
        public bool IsSaler { get; set; }
        public List<string> RoleCodes { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> PermissionReceive { get; set; } = new List<string>();
        /// <summary>
        /// Quyền teamleader ở phòng kỹ thuật
        /// </summary>
        public List<PermisTeamleaderModel> PermisTeamleader { get; set; } = new List<PermisTeamleaderModel>();
        //Thông tin token
        public UserTokenDTO TokenData { get; set; }
        public List<MenuModel> Menus { get; set; }
        public string Department { get; set; }
    }
    public class MenuModel
    {
        public string MenuName { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
    }

    public class UserTokenDTO
    {
        public string Id { get; set; }
        public string Accesstoken { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
