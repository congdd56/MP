using System.Threading.Tasks;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IAPIServices
    {
        /// <summary>
        /// Danh sách  dịch vụ
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        Task<LayDanhSachGoiDichVuResponseModel> LayDanhSachGoiDichVu(string services = "");
        /// <summary>
        /// Lay tất cả dịch vụ
        /// </summary>
        /// <returns></returns>
        Task<LayDanhSachGoiDichVuResponseModel> layTatCaGoiDichVu();
        /// <summary>
        /// Gia hạn thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponseModel> DaiLyMoiGiaHan(DaiLyMoiGiaHanRequestModel model);
        /// <summary>
        /// Tạo đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponseModel> TaoDaiLyCon(TaoDaiLyConRequestModel model);
        /// <summary>
        /// Tạo đại lý con
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<TimKiemDaiLyResponseModel> TimKiemDaiLy(string code = "");
        /// <summary>
        /// Đăng ký mới thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponseModel> DaiLyMoiGoi(DaiLyMoiGoiRequestModel model);
        /// <summary>
        /// Sửa thông tin đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponseModel> SuaThongTinDaiLy(TaoDaiLyConRequestModel model);
        /// <summary>
        /// Xóa đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<APIResponseModel> XoaDaiLy(XoaDaiLyRequestModel model);
    }
}
