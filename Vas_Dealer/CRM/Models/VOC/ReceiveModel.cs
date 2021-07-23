using VAS.Dealer.Models.CRM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC
{
    /// <summary>
    /// Thông tin tiếp nhận khi lưu ticket
    /// </summary>
    public class ReceiveRequestModel
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeniedCode { get; set; }
        public string DeniedReasion { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string OldProvider { get; set; }
        public string NewProvider { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime ProviderDate { get; set; }
        public string DNOResult { get; set; }
        public string DNODetails { get; set; }
        public string DNOOtherCondition { get; set; }
        public string Note { get; set; }
        public string DNO_RNO_Note { get; set; }
    }


    public class ReceiveIndexModel
    {

        public VOCReceiveTicketModel Ticket { get; set; }
        //public SelectList CallerCustomerType { get; set; }

        /// <summary>
        /// Lịch sử thông tin sản phẩm theo số điện thoại
        /// </summary>
        public List<VOCTicketProductHistory> Phone_ProductHistory { get; set; }
        /// <summary>
        /// Lịch sử thông tin chứng từ theo số điện thoại
        /// </summary>
        public List<VOCTicketDocHistory> Phone_DocHistory { get; set; }

        #region Tiếp nhận thông tin

        /// <summary>
        /// Kênh tiếp nhận
        /// </summary>
        public SelectList R_Channel { get; set; }
        /// <summary>
        /// Tình trạng hàng hóa
        /// </summary>
        public SelectList R_ProductStatus { get; set; }
        /// <summary>
        /// Loại sản phẩm (Loại ngành hàng)
        /// </summary>
        public SelectList R_ProductType { get; set; }
        /// <summary>
        /// Model sản phẩm
        /// </summary>
        public SelectList R_ProductModel { get; set; }
        /// <summary>
        /// Model dàn nóng
        /// </summary>
        public SelectList R_ProductModel2 { get; set; }
        /// <summary>
        /// Mầu sản phẩm
        /// </summary>
        public SelectList R_ProductColor { get; set; }
        /// <summary>
        /// Hiện trạng xử lý(Tiếp nhận)
        /// </summary>
        public SelectList R_CurrentTicketStatus { get; set; }
        /// <summary>
        /// Tình trạng hư hỏng(Thuộc MP)
        /// </summary>
        public SelectList R_CrashStatus { get; set; }

        /// <summary>
        /// Danh mục mã lỗi tab Tư vấn
        /// </summary>
        public SelectList R_DErrorCategory { get; set; }
        /// <summary>
        /// Nguồn nhận
        /// </summary>
        public SelectList R_Source { get; set; }
        /// <summary>
        /// Trạng thái KH
        /// </summary>
        public List<CategoryItemModel> R_CustomerStatus { get; set; }
        /// <summary>
        /// Mục đích
        /// </summary>
        public SelectList R_Purpose { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public SelectList R_Area { get; set; }
        /// <summary>
        /// Tỉnh, T/P cửa hàng
        /// </summary>
        public SelectList R_Province { get; set; }
        /// <summary>
        /// Quận/ Huyện cửa hàng
        /// </summary>
        public SelectList R_District { get; set; }

        public SelectList R_TicketArea { get; set; }
        public SelectList R_TicketProvince { get; set; }
        public SelectList R_TicketDistrict { get; set; }
        /// <summary>
        /// Loại cửa hàng đại lý
        /// </summary>
        public SelectList R_StoreType { get; set; }
        /// <summary>
        /// Thiết lập tab active trên giao diện VOC
        /// </summary>
        public string FirstTabDisplayReceive { get; set; }
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public SelectList R_DocutmentType { get; set; }
        /// <summary>
        /// Loại chứng từ khác -> Khi chọn khác ở loại chứng từ
        /// </summary>
        public SelectList R_OtherDocutmentType { get; set; }


        #region 1. Cửa hàng, đại lý

        public List<CategoryItemModel> R_Store_Area { get; set; }

        public List<CategoryItemModel> R_Store_Province { get; set; }

        public List<CategoryItemModel> R_Store_District { get; set; }

        /// <summary>
        /// Khi chọn loại ngành hàng có mã trùng khớp thì mới hiển thị
        /// Model dàn nóng, Số serial(dàn nóng)
        /// </summary>
        public string R_SpecicialDisplay { get; set; }

        #endregion

        #region 2. Thông tin sản phẩm

        public List<CategoryItemModel> R_Product_Brand { get; set; }


        #endregion

        #region 3. Bảo hành điện tử
        //public List<CategoryItemModel> R_Purpose { get; set; }

        //public List<CategoryItemModel> R_Purpose { get; set; }

        //public List<CategoryItemModel> R_Purpose { get; set; }

        #endregion



        #region 5. Xuất kho

        #endregion

        #region 6. Xử lý thông tin

        #endregion




        #endregion

        #region Tư vấn
        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// </summary>
        public IEnumerable<SelectListItem> D_SolutionAfterDiscus { get; set; }
        /// <summary>
        /// Chuyển tư vấn
        /// </summary>
        public SelectList D_TranferDiscus { get; set; }
        /// <summary>
        /// Danh sách tư vấn
        /// </summary>
        public List<VOCTicketDiscus> D_ListDiscus { get; set; }
        /// <summary>
        /// Danh mục mã lỗi
        /// </summary>
        public SelectList D_ErrorCategory { get; set; }
        #endregion

        #region Kỹ thuật
        /// <summary>
        /// Thông tin trạm
        /// </summary>
        public SelectList T_StationInfo { get; set; }
        //public List<VOCTicketTech> T_ListTech { get; set; }
        /// <summary>
        /// Chuyển Teamleader
        /// </summary>
        public IEnumerable<SelectListItem> T_TranTeamleader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> T_TranEmployee { get; set; }

        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// </summary>
        public IEnumerable<SelectListItem> T_SolutionAfterDiscus { get; set; }
        /// <summary>
        /// Kiểm tra với ticket và khu vực cửa hàng, KH để ra được teamleader
        /// </summary>
        public bool T_IsTeamleader { get; set; }
        #endregion

        #region Trạm xử lý
        public IEnumerable<SelectListItem> S1_Solution { get; set; }
        public IEnumerable<SelectListItem> S1_Transfer { get; set; }
        public SelectList S_StationInfo { get; set; }
        /// <summary>
        /// Trạng thái xử lý cuối cùng
        /// </summary>
        public SelectList S4_LastHandlerStatus { get; set; }
        #endregion

        #region Chăm sóc sau sửa chữa
        public SelectList C_SolutionAfterCare { get; set; }
        /// <summary>
        /// Hiện trạng/ chăm sóc sau sửa chữa
        /// </summary>
        public SelectList C_CurrentStatus { get; set; }
        /// <summary>
        /// Các bước chăm sóc
        /// </summary>
        public IEnumerable<SelectListItem> C_TakeCareStep { get; set; }
        /// <summary>
        /// Nhân viên chăm sóc
        /// </summary>
        public IEnumerable<SelectListItem> C_TakeCareBy { get; set; }
        #endregion

    }

    public class ReceiveListModel
    {
        /// <summary>
        /// Hiện trạng xử lý
        /// </summary>
        public SelectList ListCurrentStatus { get; set; }
        public SelectList ListArea { get; set; }
        public SelectList ListChannel { get; set; }
        public SelectList ListPurpose { get; set; }
        public string TicketId { get; set; }
        public string TicketStatus { get; set; }
        public string CustomerArea { get; set; }
        public string Channel { get; set; }
        public string Purpose { get; set; }
        public string PhoneNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }



    #region Yêu cầu lưu thông tin từ phòng ban
    /// <summary>
    /// Từ tổng đài
    /// </summary>
    public class ReceiveRequestCallCenterModel
    {
        public Guid Id { get; set; }
        public string IP { get; set; }
        public string PhoneCaller { get; set; }

        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeniedCode { get; set; }
        public string DeniedReasion { get; set; }

        /// <summary>
        /// Ngày đăng ký
        /// </summary>
        public string RegisterDateStr { get; set; }
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public string UpdateDateStr { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string OldProvider { get; set; }
        public string NewProvider { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string ProviderDateStr { get; set; }
        public DateTime? ProviderDate { get; set; }
        public string Note { get; set; }

        public List<string> AttachFiles { get; set; }
    }
    /// <summary>
    /// Từ DNO
    /// </summary>
    public class ReceiveRequestDepartmentModel
    {
        public Guid Id { get; set; }
        public string IP { get; set; }

        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeniedCode { get; set; }
        public string DeniedReasion { get; set; }

        /// <summary>
        /// Ngày đăng ký
        /// </summary>
        public string RegisterDateStr { get; set; }
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public string UpdateDateStr { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string NewProvider { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string ProviderDateStr { get; set; }
        public DateTime? ProviderDate { get; set; }
        public string Note { get; set; }
        public List<string> AttachFiles { get; set; }
    }
    /// <summary>
    /// Từ RNO
    /// </summary>
    public class ReceiveHandlerModel
    {
        public Guid Id { get; set; }

        public string IP { get; set; }

        public string RNOResult { get; set; }
        public string RNODetails { get; set; }
        public string RNOOtherCondition { get; set; }
    }
    public class ReceiveCloseModel
    {
        public string IP { get; set; }

        public string DNO_RNO_Note { get; set; }

        public bool IsClose { get; set; }
        public string ClosedBy { get; set; }
    }

    #endregion

    

    /// <summary>
    /// Lịch sử thông tin chứng từ
    /// </summary>
    public class VOCTicketDocHistory
    {
        public int STT { get; set; }
        /// <summary>
        /// Mã PKTTTKT
        /// </summary>
        public string PKTTTKTCode { get; set; }
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string DocNum { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        public string PurchaseDateStr { get; set; }
        /// <summary>
        /// Ngày hết hạn BH
        /// </summary>
        public string ExpiredDateStr { get; set; }
        /// <summary>
        /// Mã tình trạng
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Tình trạng hư hỏng
        /// </summary>
        public string CrashStatus { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string CustomerPhone { get; set; }
    }

    /// <summary>
    /// Dánh sách tư vấn
    /// </summary>
    public class VOCTicketDiscus
    {
        public int STT { get; set; }
        public string TicketId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// Tên nhân viên tư vấn
        /// </summary>
        public string FullNameDiscus { get; set; }
        /// <summary>
        /// Trạng thái NV, lấy theo signalr
        /// </summary>
        public string UserSignalStatus { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        /// <summary>
        /// Mã tình trạng
        /// </summary>
        public string CrashStatus_Code { get; set; }
        /// <summary>
        /// Tình trạng hư hỏng
        /// </summary>
        public string CrashStatus_Name { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tình trạng
        /// </summary>
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Danh sách kỹ thuật
    /// </summary>
    public class VOCTicketTech
    {
        public int STT { get; set; }
        /// <summary>
        /// row id cho datatable
        /// </summary>
        public string DT_RowId { get; set; }
        public string TicketId { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string CustomerPhone { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        public string CrashStatus_Code { get; set; }
        public string CrashStatus_Name { get; set; }
        /// <summary>
        /// Nội dung tư vấn
        /// </summary>
        public string D_DiscusContent { get; set; }
        /// <summary>
        /// Nội dung trao đổi với KH
        /// </summary>
        public string R_DiscusContent { get; set; }
        /// <summary>
        /// Tình trạng ticket
        /// </summary>
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsManager { get; set; }

    }

    public class VOCTicketStation
    {
        public int STT { get; set; }
        public string TicketId { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        public string CrashStatus_Code { get; set; }
        public string CrashStatus_Name { get; set; }
        /// <summary>
        /// Nội dung tư vấn
        /// </summary>
        public string D_DiscusContent { get; set; }
        /// <summary>
        /// Nội dung trao đổi với KH
        /// </summary>
        public string R_DiscusContent { get; set; }
        /// <summary>
        /// Tình trạng ticket
        /// </summary>
        public string Status { get; set; }

        public bool IsManager { get; set; }
    }





    public class VOCTicketStatinPagingModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IList<VOCTicketStation> data { get; set; }
    }


    public class VOCTicketCS
    {
        public int STT { get; set; }
        public string TicketId { get; set; }
        /// <summary>
        /// Trạng thái online offline signalr
        /// </summary>
        public bool SignalStatus { get; set; }
        public string TakeCareBy { get; set; }
        public string TakeCareBy_FullName { get; set; }

        public string Area { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Serial { get; set; }
        /// <summary>
        /// Mã tình trạng hư hỏng
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Tên tình trạng hư hỏng
        /// </summary>
        public string ErrorName { get; set; }
        /// <summary>
        /// Mã PKT TTKT
        /// </summary>
        public string CodePKTTTKT { get; set; }
        /// <summary>
        /// Tình trạng ticket
        /// </summary>
        public string CloseTicket { get; set; }
        /// <summary>
        /// Tình trạng
        /// </summary>
        public string Status { get; set; }
    }

    public class OutMessageModel
    {
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
