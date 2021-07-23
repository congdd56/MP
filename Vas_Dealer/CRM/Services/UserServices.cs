using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MP.Common;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Provider;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly ITokenServices _TokenServices;
        private readonly MP_Context _Context;
        private readonly IHubContext<CCEvents> _CCEvents;
        private readonly MM_Context _MM_Context;
        private readonly IConfiguration _Configuration;

        public UserServices(MP_Context CrmContext, ITokenServices TokenServices,
            ILogger<UserServices> logger, IHubContext<CCEvents> CCEvents, MM_Context MM_Context,
            IConfiguration Configuration)
        {
            _Context = CrmContext;
            _TokenServices = TokenServices;
            _CCEvents = CCEvents;
            _logger = logger;
            _MM_Context = MM_Context;
            _Configuration = Configuration;
        }
        public UserLogonModel GetLogonUser(string userName, string passWord, out string message)
        {
            message = string.Empty;
            try
            {
                var user = _Context.Account.Where(u => u.UserName == userName).SingleOrDefault();
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        message = "Tài khoản chưa được kích hoạt.";
                        return null;
                    }
                    if (user.IsLock)
                    {
                        message = "Tài khoản đã bị khóa.";
                        return null;
                    }

                    List<string> lstRole = new List<string>();
                    List<string> lstPermission = new List<string>();

                    lstRole = (from a in user.AccountRole select a.Role.Name).ToList();
                    //Lấy danh sách role id, cần sửa lại chỗ này lấy theo lazyloading
                    if (user.UserName.ToLower() == "admin")
                    {
                        lstPermission = (from s in _Context.Permission select s.Code).ToList();

                    }
                    else
                    {
                        List<int> roleIds = (from a in user.AccountRole select a.Role.Id).ToList();
                        if (roleIds != null && roleIds.Count > 0)
                        {
                            List<int> permisstionIds = (from s in _Context.RolePermission
                                                        where roleIds.Contains(s.IdRole)
                                                        select s.IdPermission).ToList();
                            if (permisstionIds != null && permisstionIds.Count > 0)
                            {
                                lstPermission = (from s in _Context.Permission
                                                 where permisstionIds.Contains(s.Id)
                                                 select s.Code).ToList();
                            }
                        }
                    }


                    string passwordHash = GeneratorPassword.EncodePassword(passWord, 1, user.PasswordSalt);
                    if (user.Password == passwordHash)
                    {
                        AddAccountOnline(user);
                        AccountLog(user.UserName, AccountLoginTime.Login);

                        return new UserLogonModel()
                        {
                            UserId = user.Id,
                            Email = user.Email,
                            FullName = user.FullName,
                            UserName = user.UserName,
                            TokenData = _TokenServices.GetUserToken(user),
                            RoleCodes = lstRole,
                            Permissions = lstPermission,
                            Menus = new List<MenuModel>()
                        };
                    }
                    message = "Sai thông tin mật khẩu";
                    return null;
                }
                message = "Sai thông tin tài khoản";
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }
            return null;
        }

        /// <summary>
        /// Xóa user đăng nhập hệ thống
        /// </summary>
        /// <param name="userName"></param>
        public void RemoveAccountOnline(string userName)
        {
            #region + Lưu vào ram thông tin tài khoản online
            var item = _MM_Context.OnlineUser.Where(x => x.UserName == userName).ToList();
            if (item != null && item.Count > 0)
            {
                _MM_Context.OnlineUser.RemoveRange(item);
                _MM_Context.SaveChanges();
            }
            #endregion
        }

        /// <summary>
        /// Thêm user đăng nhập hệ thống
        /// </summary>
        /// <param name="user"></param>
        void AddAccountOnline(MP_Account user)
        {
            #region + Lưu vào ram thông tin tài khoản online
            var online = new MM_OnlineUser()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                LoginDate = DateTime.Now
            };
            _MM_Context.OnlineUser.Add(online);
            _MM_Context.SaveChanges();

            #endregion
        }

        /// <summary>
        /// Lấy danh sách tài khản theo tên đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<AccountModel> GetListAccount(string userName)
        {
            var query = from s in _Context.Account
                        where ((s.UserName.Contains(userName) || string.IsNullOrEmpty(userName)) && s.IsDeleted == false)
                        select s;
            var onlineItem = _MM_Context.OnlineUser.Select(s => s.UserName).ToList();

            var item = query.Select(s => new AccountModel()
            {
                Id = s.Id,
                UserName = s.UserName,
                Email = s.Email,
                FullName = s.FullName,
                PhoneNumber = s.PhoneNumber,
                IsLock = s.IsLock,
                IsActive = s.IsActive,
                Roles = s.AccountRole != null ? s.AccountRole.Select(s => s.Role != null ? s.Role.Name : string.Empty).ToList() : new List<string>(),
                SignalStatus = onlineItem.Contains(s.UserName),
                VendorStr = s.Vendor != null ? (s.Vendor.Branch_code + " - " + s.Vendor.Branch_name) : string.Empty
            }).ToList();


            return item;
        }

        /// <summary>
        /// Thêm mới tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userName">Tài khoản tạo user</param>
        /// <returns></returns>
        public object AddAccount(AccountModel obj, string userName)
        {
            try
            {
                if (_Context.Account.Any(x => x.UserName == obj.UserName))
                    return new { status = "err-exit" };
                MP_Account account = new MP_Account
                {
                    UserName = obj.UserName,
                    Email = "default@mptelecom.com.vn",
                    FullName = obj.FullName,
                    IsLock = false,
                    IsActive = true, 
                    PasswordFormat = 1,
                    PasswordSalt = "pwEV5VXpyGV4PavcWzBn3Q==",
                    CreatedBy = userName,
                    PhoneNumber = "0947999999",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    VendorId = obj.Vendor,
                    IsPriviot = obj.IsPriviot,
                    AccountRole = new List<MP_AccountRole>()
                };
                //account.Password = GeneratorPassword.EncodePassword(obj.PassWord, 1, account.PasswordSalt);
                //gan cung Abc123456 
                account.Password = "lppYae8LBeRJvxvvZ0/iIycaJ9E=";

                account.AccountRole.Add(new MP_AccountRole
                {
                    AccId = account.Id,
                    RoleId = 2,
                });

                _Context.Account.Add(account); 
                _Context.SaveChanges();
                if (account.Id > 0)
                {
                    Lotto lotto = new Lotto
                    {
                        AccountId = account.Id,
                        Score = 0,
                        IsClosed = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = "",
                        IsDeleted = false
                    }; 
                    _Context.Lotto.Add(lotto);
                }
                _Context.SaveChanges();

                return new { status = "ok" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }
        /// <summary>
        /// Cập nhật tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object UpdateAccount(AccountModel obj)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (account == null) return new { status = "not-found" };
            if (_Context.Account.Any(x => x.UserName == obj.UserName && x.Id != obj.Id)) return new { status = "err-exit" };
            account.Email = obj.Email;
            account.FullName = obj.FullName;
            account.IsLock = false;
            account.PhoneNumber = obj.PhoneNumber;
            account.VendorId = obj.Vendor;
            account.IsPriviot = obj.IsPriviot;

            _Context.SaveChanges();
            return new { status = "ok" };
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountModel GetAccountById(int id)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == id).FirstOrDefault();
            if (account == null) return null;
            return new AccountModel
            {
                UserName = account.UserName,
                FullName = account.FullName,
                Id = account.Id,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                IsActive = account.IsActive,
                IsDeleted = account.IsDeleted,
                IsPriviot = account.IsPriviot,
            };
        }

        /// <summary>
        /// Lấy mã code recover password tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="conf"></param>
        /// <param name="userEmail"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool GetCodeRecover(string userName, MailConfigModel conf, string userEmail, out RecoverPasswordModel msg)
        {
            msg = new RecoverPasswordModel();
            #region VALIDATE
            msg.Msg = string.Empty;
            var item = _Context.Account.Where(x => x.UserName == userName).FirstOrDefault();
            if (item == null)
            {
                msg.Msg = "Không tim thấy thông tin tài khoản.";
                return false;
            }
            if (!item.IsActive)
            {
                msg.Msg = "Tài khoản chưa được active.";
                return false;
            }
            if (item.IsLock)
            {
                msg.Msg = "Tài khoản đã bị khóa.";
                return false;
            }
            if (item.IsDeleted)
            {
                msg.Msg = "Tài khoản đã xóa.";
                return false;
            }
            if (string.IsNullOrEmpty(item.Email))
            {
                msg.Msg = "Tài khoản chưa đăng ký email.";
                return false;
            }
            if (!MPHelper.IsValidEmail(item.Email))
            {
                msg.Msg = "Tài khoản sai định dạng email.";
                return false;
            }
            #endregion

            #region Handler
            var totp = new Totp(Encoding.ASCII.GetBytes(userName), totpSize: 6);


            var checkSend = MPHelper.SendMail(conf.SSLoTLS, conf.UserName, conf.Password, conf.Host, conf.Port, userEmail,
               subject: "[DPL] Mã code thay đổi mật khẩu CRM chăm sóc khách hàng.",
               content: $"Dear {userName},<br/>Mã code được sử dụng để thay đổi mật khẩu hiện tại trên CRM của khách hàng là: <b>{totp.ComputeTotp()}</b>", out string messageSendMail);
            if (!checkSend)
            {
                _logger.LogError("SendMailStation: " + messageSendMail);
                msg.UserName = item.UserName;
                msg.Msg = "Không thể gửi mail đến email của tài khoản." + messageSendMail;
                return true;
            }


            var checkInt = int.TryParse(_Configuration.GetSection("UserTokenSetting:OTPRecoverPassword").Value, out int expiredValue);
            if (!checkInt) expiredValue = 15;

            SaveSendMail(new MP_RecoverPassword()
            {
                CreatedDate = DateTime.Now,
                Email = item.Email,
                ExpiredDate = DateTime.Now.AddMinutes(expiredValue),
                IsDeleted = false,
                IsHandler = false,
                IsSent = checkSend,
                SendSuccess = checkSend,
                Totp = totp.ComputeTotp(),
                UserName = item.UserName
            });



            msg.UserName = item.UserName;
            return true;
            #endregion

        }

        /// <summary>
        /// Lấy lại mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="code"></param>
        /// <param name="newPassword"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool RecoveryPassword(string userName, string code, string newPassword, out string msg)
        {
            msg = string.Empty;
            var item = _Context.RecoverPassword.Where(x => x.UserName == userName && !x.IsDeleted
                && x.Totp == code && !x.IsHandler).FirstOrDefault();
            if (item == null)
            {
                msg = "Không tìm thấy thông tin OTP";
                return false;
            }
            if (item.ExpiredDate < DateTime.Now)
            {
                msg = "OTP đã hết hạn sử dụng";
                return false;
            }

            item.IsHandler = true;
            item.IsDeleted = true;
            item.HandlerDate = DateTime.Now;

            var user = _Context.Account.Where(x => x.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                msg = "Không tìm thấy thông tin tài khoản";
                return false;
            }

            user.Password = GeneratorPassword.EncodePassword(newPassword, 1, user.PasswordSalt);

            _Context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object UpdateInfoAccount(AccountModel obj)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (account == null) return null;
            if (_Context.Account.Any(x => x.UserName == obj.UserName && x.Id != obj.Id)) return null;
            account.UserName = obj.UserName;
            account.Email = obj.Email;
            account.FullName = obj.FullName;
            account.PhoneNumber = obj.PhoneNumber;
            _Context.SaveChanges();
            return new { status = "ok" };
        }
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object DeleteAccount(int Id, string userName)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return new { status = "not-found" };
            if (account.UserName == userName) return new { status = "not-allow" };
            account.IsDeleted = true;
            _Context.SaveChanges();
            return new { status = "ok" };
        }
        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool ResetPassword(int Id, out string pw)
        {
            pw = _Configuration.GetSection("UserTokenSetting:DefaultPassword").Value;
            MP_Account account = _Context.Account.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return false;
            account.Password = GeneratorPassword.EncodePassword(pw, 1, account.PasswordSalt);
            _Context.SaveChanges();
            return true;
        }
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="passwordOld"></param>
        /// <param name="passwordNew"></param>
        /// <returns></returns>
        public object ChangePassword(int Id, string passwordOld, string passwordNew)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return new { status = "not-found" };
            var passOld = GeneratorPassword.EncodePassword(passwordOld, 1, account.PasswordSalt);
            if (passOld != account.Password)
            {
                return new { status = "password-failed" };
            }
            account.Password = GeneratorPassword.EncodePassword(passwordNew, 1, account.PasswordSalt);
            _Context.SaveChanges();
            return new { status = "ok" };
        }
        /// <summary>
        /// Mở/khóa tài khoản
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IsLock"></param>
        /// <returns></returns>
        public object UnLockAccount(int Id, bool IsLock)
        {
            MP_Account account = _Context.Account.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return new { status = "not-found" };
            account.IsLock = !IsLock;
            _Context.SaveChanges();
            return new { status = "ok" };
        }
        public object GetInfoAccountById(int Id)
        {
            var account = (from a in _Context.Account where a.Id == Id select new { a.Id, a.IsActive, a.UserName, a.FullName, a.Email, a.PhoneNumber, a.VendorId,a.IsDeleted,a.IsPriviot }).FirstOrDefault();
            var lstRole = (from a in _Context.Account join b in _Context.AccountRole on a.Id equals b.AccId where a.Id == Id select new { b.RoleId }).ToList();
            return new { status = "ok", value = account, role = lstRole };
        }

        /// <summary>
        /// Lấy danh sách tài khoản cho select2
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public SelectList GetAllAccount(string selected = "")
        {
            var item = _Context.Account.OrderBy(o => o.UserName)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.UserName} - {s.FullName}",
                    Value = s.UserName
                }).ToList();
            return new SelectList(item, "Value", "Text", selected);
        }
        /// <summary>
        /// Lấy danh sách tài khoản cho select2
        /// Do anh Tiến không đặt tiếp đầu ngữ tài khoản CIC giống với CRM 
        /// nên cần đổi để có thể tìm kiếm theo agent được
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public SelectList GetAllAccountCIC(string selected = "")
        {
            var item = _Context.Account.OrderBy(o => o.UserName)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.UserName} - {s.FullName}",
                    Value = s.UserName.Replace("mp_", "dpl_")
                }).ToList();
            return new SelectList(item, "Value", "Text", selected);
        }

        void SaveSendMail(MP_RecoverPassword model)
        {
            try
            {
                _Context.RecoverPassword.Add(model);
                _Context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

        }

        /// <summary>
        /// Kiểm tra tài khoản có trên hệ thống, được active và không bị khóa
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckExistUserByUserName(string userName)
        {
            return _Context.Account.Any(x => x.UserName.ToLower() == userName.ToLower() && x.IsActive && !x.IsLock);
        }

        /// <summary>
        /// Kiểm tra tài khoản có email hoặc sai định dạng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool GetEmailByUserName(string userName, out string message)
        {
            message = string.Empty;
            var item = _Context.Account.Where(x => x.UserName.ToLower() == userName.ToLower() && x.IsActive && !x.IsLock).FirstOrDefault();
            if (item == null || string.IsNullOrEmpty(item.Email)) return false;

            if (!MPHelper.IsValidEmail(item.Email))
                return false;

            message = item.Email;
            return true;
        }

        /// <summary>
        /// Lưu thời gian đăng nhập, đăng xuất
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        public void AccountLog(string userName, string type)
        {
            _Context.AccountLoginTime.Add(new MP_AccountLoginTime()
            {
                Id = Guid.NewGuid(),
                LogTime = DateTime.Now,
                Type = type,
                UserName = userName
            });
            _Context.SaveChanges();
        }
    }
}
