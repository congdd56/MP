using System.Collections.Generic;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IVASServices
    {
        /// <summary>
        /// Lịch sử đăng ký mới thuê bao theo tài khoản
        /// </summary>
        /// <param name="username"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PagingResultModel> GetRegisterByUser(string username, PagingRequestModel model);
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ValidateCreateVendor(TaoDaiLyConRequestModel model, out string message);
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ValidateUpdateVendor(TaoDaiLyConRequestModel model, out string message);
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ValidateDeleteVendor(string code, out string message);
        /// <summary>
        /// Delete đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> DeleteVendor(XoaDaiLyRequestModel model, string userName);
        /// <summary>
        /// Lưu thông tin đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> CreateVendor(TaoDaiLyConRequestModel model, string userName);
        /// <summary>
        /// Lưu thông tin đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> UpdateVendor(TaoDaiLyConRequestModel model, string userName);

        /// <summary>
        /// Danh sách đại lý đã tạo
        /// </summary>
        /// <returns></returns>
        Task<List<VendorResponseModel>> ListVendor();

        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagingResultModel2> GetRegister(PagingAllHisRegisRequestModel page);
        /// <summary>
        /// Lấy đại lý theo quyền
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="allowAll"></param>
        /// <returns></returns>
        Task<List<Select2Model>> GetVendorByPermis(string userName, bool allowAll = false);
        /// <summary>
        /// Lấy đại lý
        /// </summary>
        /// <returns></returns>
        Task<List<Select2Model>> GetAllVendor();
        /// <summary>
        /// Lưu thông tin đăng ký
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="tradeKey"></param>
        /// <returns></returns>
        Task SaveRegis(DaiLyMoiGoiRequestModel model, string tradeKey, string userName);
        /// <summary>
        /// Gia hạn thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="tradeKey"></param>
        /// <returns></returns>
        Task SaveExtend(DaiLyMoiGiaHanRequestModel model, string tradeKey, string userName);
        /// <summary>
        /// Lấy thông tin đại lý theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<VendorResponseModel> GetVendorByCode(string code);
    }
}
