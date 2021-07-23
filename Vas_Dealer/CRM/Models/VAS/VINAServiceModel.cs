using Newtonsoft.Json;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VAS.VINAService
{
    public class VINAServiceModel
    {
        public string Uri { get; set; }
        public Login Login { get; set; }
        public LayDanhSachGoiDichVu LayDanhSachGoiDichVu { get; set; }
        public TaoDaiLyCon TaoDaiLyCon { get; set; }
        public TimKiemDaiLy TimKiemDaiLy { get; set; }
        public DaiLyMoiGoi DaiLyMoiGoi { get; set; }
        public SuaThongTinDaiLy SuaThongTinDaiLy { get; set; }
        public XoaDaiLy XoaDaiLy { get; set; }
        public DaiLyMoiGiaHan DaiLyMoiGiaHan { get; set; }
    }

    public class APIResponseModel
    {
        /// <summary>
        /// Mã lỗi trả về(0: thành công, còn lại là lỗi)
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        /// <summary>
        /// Thông báo chi tiết thành công hoặc lỗi
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        public string TradeKey { get; set; }
    }

    #region Đăng nhập
    public class Login
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginResponseModel
    {
        /// <summary>
        /// Mã lỗi trả về(0: thành công, còn lại là lỗi)
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        /// <summary>
        /// Thông báo chi tiết thành công hoặc lỗi
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// Trả về khi đăng nhập thành công
        /// </summary>
        public string Session { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public virtual string TradeKey { get => Message.Replace("Đăng nhập thành công, mã giao dịch ", ""); }
    }
    #endregion

    #region Danh sách dịch vụ
    public class LayDanhSachGoiDichVu
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        public string p_service_code { get; set; }
    }
    public class LayDanhSachGoiDichVuResponseModel
    {
        /// <summary>
        /// Mã lỗi trả về(0: thành công, còn lại là lỗi)
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        [JsonProperty("result")]
        public List<LayDanhSachGoiDichVuResponseDetailModel> Result { get; set; }
    }
    public class LayDanhSachGoiDichVuResponseDetailModel
    {
        public string STT { get; set; }
        public string LOAI_DVU { get; set; }
        public string DVU { get; set; }
        public string GIA { get; set; }
        public string TTHAI { get; set; }
        public string MT { get; set; }
    }

    #endregion

    #region Tạo đại lý con
    public class TaoDaiLyCon
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string branch_mobile { get; set; }
        public string branch_sales_code { get => branch_code; }
        public string tel { get => branch_mobile; }
        public string fax { get => ""; }
        public string rep { get => ""; }
        public string connact_no { get => ""; }
        public string email { get => ""; }
        public string dchi { get => "MPTelecom"; }
        public string description { get => "MP Group"; }
    }

    public class TaoDaiLyConRequestModel
    {
        /// <summary>
        /// Mã đại lý con cần tạo
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        /// Tên đại lý con cần tạo
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// Số điện thoại đại lý con cần tạo mới
        /// </summary>
        public string branch_mobile { get; set; }
        public string branch_sales_code { get => branch_code; }
    }

    #endregion

    #region Tìm kiếm đại lý
    public class TimKiemDaiLy
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        /// <summary>
        /// Mã đại lý, p_code = "" thì lấy tât cả danh sách đại lý thuộc đại lý đăng nhập
        /// </summary>
        public string p_code { get; set; }
    }

    public class TimKiemDaiLyResponseModel
    {
        /// <summary>
        /// Mã lỗi trả về(0: thành công, còn lại là lỗi)
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        [JsonProperty("result")]
        public List<TimKiemDaiLyResponseDetailModel> Result { get; set; }
    }
    public class TimKiemDaiLyResponseDetailModel
    {
        public string STT { get; set; }
        public string BRANCH_ID { get; set; }
        public string BRANCH_TYPE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_SALES_CODE { get; set; }
        public string BRANCH_P_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_MOBILE { get; set; }
        public string BRANCH_TEL { get; set; }
        public string CREATED_DATE { get; set; }
        public string ADDRESS { get; set; }
        public string STATUS { get; set; }
    }
    #endregion

    #region Đại lý mời gói
    public class DaiLyMoiGoi
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        /// <summary>
        /// Số thuê bao đại lý muốn mời
        /// </summary>
        public string so_tb { get; set; }
        /// <summary>
        /// Gói dịch vụ muốn mời
        /// </summary>
        public string service_code { get; set; }
        /// <summary>
        /// Mã đại lý
        /// </summary>
        public string branch_code { get; set; }
    }

    public class DaiLyMoiGoiRequestModel
    {
        /// <summary>
        /// Số thuê bao đại lý muốn mời
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gói dịch vụ muốn mời
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// Mã đại lý
        /// </summary>
        public string Branch { get; set; }
    }
    #endregion

    #region Sửa đại lý
    public class SuaThongTinDaiLy
    {
        public string service { get; set; }
        public string session { get; set; }
        /// <summary>
        /// Mã đại lý con
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        /// Tên đại lý con
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// Số điện thoại đại lý con
        /// </summary>
        public string branch_mobile { get; set; }
        /// <summary>
        /// Số tel
        /// </summary>
        public string branch_tel { get => branch_mobile; }
        public string branch_rep { get => ""; }
        public string branch_fax { get => ""; }
        public string email { get => ""; }
        public string contract_no { get => ""; }
        /// <summary>
        /// Địa chỉ đại lý con
        /// </summary>
        public string address { get => "MPTelecom"; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get => "MP Group"; }
    }

    #endregion

    #region Xóa đại lý

    public class XoaDaiLy
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        /// <summary>
        /// Mã đại lý con
        /// </summary>
        public string branch_code { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; set; }
    }


    public class XoaDaiLyRequestModel
    {
        /// <summary>
        /// Mã đại lý con
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// lý do
        /// </summary>
        public string Reasion { get; set; }
    }
    #endregion

    #region Đại lý gia hạn thuê bao

    public class DaiLyMoiGiaHan
    {
        public string service { get; set; }
        public string Method { get; set; }
        public string session { get; set; }
        /// <summary>
        /// Số thuê bao đại lý muốn mời gia hạn
        /// </summary>
        public string so_tb { get; set; }
        /// <summary>
        /// Gói dịch vụ muốn mời gia hạn
        /// </summary>
        public string service_code { get; set; }
        /// <summary>
        /// Mã đại lý
        /// </summary>
        public string branch_code { get; set; }
    }

    public class DaiLyMoiGiaHanRequestModel
    {
        /// <summary>
        /// Số thuê bao đại lý muốn mời gia hạn
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gói dịch vụ muốn mời gia hạn
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// Mã đại lý
        /// </summary>
        public string Branch { get; set; }
    }

    #endregion







}
