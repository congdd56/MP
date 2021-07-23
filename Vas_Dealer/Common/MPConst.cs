namespace MP.Common
{
    public static class MPConst
    {

        /// <summary>
        /// Cho phép truy cập màn hình quản lý tài khoản
        /// </summary>
        public const string view_account = "view_account";
        /// <summary>
        /// Cho phép truy cập màn hình quản lý quyền
        /// </summary>
        public const string view_role = "view_role";
        /// <summary>
        /// Cho phép truy cập màn hình danh mục
        /// </summary>
        public const string view_category = "view_category";
        /// <summary>
        /// Cho phép truy cập màn hình cấu hình mail
        /// </summary>
        public const string view_mail_config = "view_mail_config";
        /// <summary>
        /// Cho phép truy cập màn hình quản lý mã lỗi
        /// </summary>
        public const string view_error_code = "view_error_code";
        /// <summary>
        /// Cho phép xem tài khoản đăng nhập hệ thống
        /// </summary>
        public const string view_login_account = "view_login_account";
        /// <summary>
        /// Cho phép chỉnh sửa thông tin khách hàng
        /// </summary>
        public const string edit_manager_customer = "edit_manager_customer";
        /// <summary>
        /// Cho phép truy cập màn hình quản lý khách hàng
        /// </summary>
        public const string view_manager_customer = "view_manager_customer";
        /// <summary>
        /// Cho phép quản lý nhóm điện thoại viên
        /// </summary>
        public const string manager_group_agent = "manager_group_agent";

        #region Đăng ký mới thuê bao
        /// <summary>
        /// Cho phép tạo mới thuê bao
        /// </summary>
        public const string vas_register = "vas_register";
        /// <summary>
        /// Cho phép tạo mới thuê bao với tất cả đại lý
        /// </summary>
        public const string vas_register_all = "vas_register_all";
        /// <summary>
        /// Cho phép truy cập màn đăng ký mới thuê bao
        /// </summary>
        public const string vas_view_register = "vas_view_register";
        /// <summary>
        /// Cho phép gia hạn thuê bao
        /// </summary>
        public const string vas_extend = "vas_extend";
        /// <summary>
        /// Cho phép gia hạn thuê bao với tất cả đại lý
        /// </summary>
        public const string vas_extend_all = "vas_extend_all";
        #endregion

        #region Lịch sử đăng ký
        /// <summary>
        /// Cho phép truy cập màn hình lịch sử đăng ký
        /// </summary>
        public const string his_view_his = "his_view_his";
        /// <summary>
        /// Cho phép lịch sử đăng ký tất cả đại lý
        /// </summary>
        public const string his_view_all = "his_view_all";

        #endregion

        #region Dịch vụ
        /// <summary>
        /// Cho phép truy cập màn hình danh sách dịch vụ
        /// </summary>
        public const string service_view = "service_view";

        #endregion

        #region Quản lý đại lý
        /// <summary>
        /// Cho phép truy cập màn hình đại lý
        /// </summary>
        public const string vendor_view = "vendor_view";
        /// <summary>
        /// Cho phép xóa đại lý
        /// </summary>
        public const string vendor_delete = "vendor_delete";
        /// <summary>
        /// Cho phép cập nhật đại lý
        /// </summary>
        public const string vendor_edit = "vendor_edit";
        /// <summary>
        /// Cho phép tạo mới đại lý
        /// </summary>
        public const string vendor_add = "vendor_add";

        #endregion




    }
    public static class MPAPIStatusErr
    {
        public enum Code : int
        {
            /// <summary>
            /// Không tìm thấy công ty
            /// </summary>
            NoCompany = 101,
            /// <summary>
            /// Tên đăng nhập đã tồn tại vui lòng nhập lại
            /// </summary>
            UserNameExits = 102,
            /// <summary>
            /// Lỗi hệ thống
            /// </summary>
            SystemError = 103,
            /// <summary>
            /// Hệ thống đăng ký không sẵn sàng hoặc tạm dừng
            /// </summary>
            RegisSystemUnavailabe = 104,
            /// <summary>
            /// Không nhập đầy đủ thông tin
            /// </summary>
            NotEnoughInformation = 105,
            /// <summary>
            /// Sai mã công ty
            /// </summary>
            WrongCompanyCode = 106,
            /// <summary>
            /// Sai định dạng thời gian
            /// </summary>
            FormatDate = 107,
            /// <summary>
            /// Code ứng tuyển sai hoặc chưa được kích hoạt
            /// </summary>
            ErrorRecruitmentCode = 108,
            /// <summary>
            /// Code ứng tuyển đã được sử dụng
            /// </summary>
            ErrorRecruitmentInUsed = 109,
            /// <summary>
            /// Không tìm thấy dữ liệu cho việc ứng tuyển
            /// </summary>
            NotRecruitmentData = 110,
            /// <summary>
            /// Email đã tồn tại.
            /// </summary>
            EmailAccountExits = 111,
            /// <summary>
            /// Tài khoản không tồn tại
            /// </summary>
            UserNameNotFound = 112,
            /// <summary>
            /// Không tìm thấy email trong tài khoản
            /// </summary>
            EmailAccountNotFound = 113,
            /// <summary>
            /// Tài khoản đã kích hoạt. Vui lòng đăng nhập
            /// </summary>
            AccountHadActive = 114,
            /// <summary>
            /// Sai mã kích hoạt
            /// </summary>
            ErrorCodeActiveAccount = 115,
            /// <summary>
            /// Mã lấy lại mật khẩu đã hết hạn
            /// </summary>
            PasswordRecoverCodeExpire = 116,
            /// <summary>
            /// Mật khẩu không đúng định dạng
            /// </summary>
            PasswordWrongFormat = 117,
            /// <summary>
            /// Không tìm thấy chiến dịch
            /// </summary>
            CampaignNotFound = 118,
            /// <summary>
            /// Hết dữ liệu gọi ra
            /// </summary>
            OutOfDataCall = 119,
            /// <summary>
            /// Mật khẩu cũ không đúng
            /// </summary>
            OldPassWrong = 120,
            /// <summary>
            /// Trùng thông tin đăng ký ứng viên
            /// </summary>
            RecDuplicateInfo = 121,
            /// <summary>
            /// Yêu cầu nhập thông tin cuộc gọi
            /// </summary>
            RequireCallInfo = 122,
            /// <summary>
            /// Không tìm thấy thông tin kết nối dữ liệu
            /// </summary>
            DataServerNotFound = 123,
            /// <summary>
            /// Sai ngày giờ hẹn gọi lại
            /// </summary>
            ErrorAppoimentDate = 124,
            /// <summary>
            /// Số điện thoại không được trống
            /// </summary>
            RequiredPhone = 125,
            /// <summary>
            /// Yêu cầu nhập trạng thái
            /// </summary>
            RequiredStatus = 126,

            #region API Intergration
            /// <summary>
            /// Thành công
            /// </summary>
            Ok = 200,
            /// <summary>
            /// Lỗi hệ thống
            /// </summary>
            ErrorSystem = 500,
            /// <summary>
            /// Không tìm thấy mã sự vụ
            /// </summary>
            NotFoundTicket = 404,
            /// <summary>
            /// Thiếu thông tin 'DataId'
            /// </summary>
            RequiredDataId_5 = 5001,
            /// <summary>
            /// Thiếu thông tin 'CaseID'
            /// </summary>
            RequiredCaseID_5 = 5002,
            /// <summary>
            /// Thiếu thông tin 'FileName'
            /// </summary>
            RequiredFileName_5 = 5003,

            #region 1. MPLoadPKTTTKT 
            /// <summary>
            /// Thiếu thông tin 'NumSeqPKTTTKT'
            /// </summary>
            RequiredNumSeqPKTTTKT_5 = 5004,
            /// <summary>
            /// Thiếu thông tin 'PKTStatus'
            /// </summary>
            RequiredPKTStatus_5 = 5005,
            /// <summary>
            /// Sai định dạng DateClose, yêu cầu 'dd/MM/yyyy'
            /// </summary>
            FormatDateClose_5 = 5006,
            /// <summary>
            /// Sai định dạng DateCreate, yêu cầu 'dd/MM/yyyy'
            /// </summary>
            FormatDateCreate_5 = 5007,

            #endregion

            #region 2. MPLoadSCLKPT
            /// <summary>
            /// Thiếu thông tin 'TTBHPhieuKTTTKT'
            /// </summary>
            RequiredTTBHPhieuKTTTKT_6 = 6001,
            /// <summary>
            /// Thiếu thông tin 'TTBHPhieuSC'
            /// </summary>
            RequiredTTBHPhieuSC_6 = 6002,

            #endregion

            #region 3. NumSeqPDNCLKPT
            /// <summary>
            /// Thiếu thông tin 'NumSeqPDNCLKPT'
            /// </summary>
            RequiredNumSeqPDNCLKPT_7 = 7001,
            /// <summary>
            /// Sai định dạng TransDateReceivedPTLK, yêu cầu 'dd/MM/yyyy'
            /// </summary>
            FormatTransDateReceivedPTLK_7 = 7002,
            /// <summary>
            /// Sai định dạng TransDateRecivedTBH, yêu cầu 'dd/MM/yyyy'
            /// </summary>
            FormatTransDateRecivedTBH_7 = 7003,

            #endregion

            #endregion
        }
    }
    public static class AccountLoginTime
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        public static string Login = "Login";
        /// <summary>
        /// Đăng xuất
        /// </summary>
        public static string Logout = "Logout";
    }
}
