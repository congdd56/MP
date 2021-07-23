using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IFTPServices
    {
        /// <summary>
        /// Lấy danh sách file name trong folder
        /// </summary>
        /// <returns></returns>
        List<string> GetAllFileName();
        /// <summary>
        /// Tải file về
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> GetFiles(List<string> fileNames, string userName);
        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRegis1Day
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task InitDataRegis1Day(DateTime fromDate, DateTime toDate, string userName);
        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRegis1Day
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task InitDataRegis30(DateTime fromDate, DateTime toDate, string userName);
        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRenew1Day
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task InitDataRenew1Day(DateTime fromDate, DateTime toDate, string userName);
    }
}
