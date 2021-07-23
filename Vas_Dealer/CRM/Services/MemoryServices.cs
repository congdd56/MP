using VAS.Dealer.Models.CRM;
using VAS.Dealer.Provider;
using VAS.Dealer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace VAS.Dealer.Services
{
    public class MemoryServices : IMemoryServices
    {
        private readonly MM_Context _MMContext;
        public MemoryServices(MM_Context MM_Context)
        {
            _MMContext = MM_Context;
        }

        /// <summary>
        /// Lấy danh sách tài khoản online
        /// </summary>
        /// <returns></returns>
        public List<UserOnlineModel> GetUserOnline()
        {
            var model = _MMContext.OnlineUser.Select(s => new UserOnlineModel()
            {
                FullName = s.FullName,
                LoginDate = s.LoginDate,
                UserName = s.UserName
            }).ToList();

            model = model.Select((s, i) => new UserOnlineModel()
            {
                STT = (i + 1),
                UserName = s.UserName,
                LoginDate = s.LoginDate,
                FullName = s.FullName
            }).ToList();
            return model;
        }

        /// <summary>
        /// Kiểm tra tài khoản đang online
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckOnline(string userName)
        {
            return _MMContext.OnlineUser.Any(x => x.UserName == userName);
        }
    }
}
