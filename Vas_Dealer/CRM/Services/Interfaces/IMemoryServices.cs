using VAS.Dealer.Models.CRM;
using System.Collections.Generic;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IMemoryServices
    {
        /// <summary>
        /// Lấy danh sách tài khoản online
        /// </summary>
        /// <returns></returns>
        List<UserOnlineModel> GetUserOnline();
        /// <summary>
        /// Kiểm tra tài khoản đang online
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckOnline(string userName);
    }
}
