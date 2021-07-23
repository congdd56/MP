using MP.Common;
using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.CRM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IUserServices
    {
        UserLogonModel GetLogonUser(string userName, string passWord, out string message);
        /// <summary>
        /// Danh sách quản lý tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<AccountModel> GetListAccount(string userName);
        /// <summary>
        /// Lấy danh sách tài khoản cho select2
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        SelectList GetAllAccount(string selected = "");
        /// <summary>
        /// Lấy danh sách tài khoản cho select2
        /// Do anh Tiến không đặt tiếp đầu ngữ tài khoản CIC giống với CRM 
        /// nên cần đổi để có thể tìm kiếm theo agent được
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        SelectList GetAllAccountCIC(string selected = "");
        object AddAccount(AccountModel obj, string userName);
        object UpdateAccount(AccountModel obj);
        object UpdateInfoAccount(AccountModel obj);

        /// <summary>
        /// Kiểm tra tài khoản có trên hệ thống, được active và không bị khóa
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckExistUserByUserName(string userName);

        /// <summary>
        /// Kiểm tra tài khoản có email hoặc sai định dạng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool GetEmailByUserName(string userName, out string message);

        AccountModel GetAccountById(int id);
        /// <summary>
        /// Lấy mã code recover password tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="conf"></param>
        /// <param name="userEmail"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool GetCodeRecover(string userName, MailConfigModel conf, string userEmail, out RecoverPasswordModel msg);
        /// <summary>
        /// Lấy lại mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="code"></param>
        /// <param name="newPassword"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool RecoveryPassword(string userName, string code, string newPassword, out string msg);
        object DeleteAccount(int Id, string userName);
        bool ResetPassword(int Id, out string pw);
        object ChangePassword(int Id, string passwordOld, string passwordNew);
        object UnLockAccount(int Id, bool IsLock);
        object GetInfoAccountById(int Id);
        void RemoveAccountOnline(string userName);

        /// <summary>
        /// Lưu thời gian đăng nhập, đăng xuất
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        void AccountLog(string userName, string type);
    }
}
