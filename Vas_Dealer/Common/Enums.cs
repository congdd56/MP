namespace MP.Common
{
    public enum VOC_UploadType
    {
        /// <summary>
        /// File hóa đơn
        /// </summary>
        Invoice = 1,
        /// <summary>
        /// File mẫu sản phẩm
        /// </summary>
        Temp = 2,
        /// <summary>
        /// File tình trạng hàng hóa
        /// </summary>
        TTHH = 3
    }
    public enum VOC_Tab
    {
        /// <summary>
        /// Tổng đài
        /// </summary>
        CallCenter = 1,
        /// <summary>
        /// Tư vấn
        /// </summary>
        Discus = 2,
        /// <summary>
        /// Kỹ thuật
        /// </summary>
        Tech = 3,
        /// <summary>
        /// Chăm sóc
        /// </summary>
        TakeCare = 4
    }

    public enum VOC_RequestReceiveType
    {
        /// <summary>
        /// Lưu thông tin tiếp nhận
        /// </summary>
        Request = 1,
        /// <summary>
        /// Lưu thông tin xử lý
        /// </summary>
        Handler = 2
    }

    public enum VOC_Button
    {
        /// <summary>
        /// Button lưu thông tin yêu cầu, RNO
        /// </summary>
        Receive = 1,
        /// <summary>
        /// Button lưu thông tin xử lý, DNO
        /// </summary>
        Handler = 2,
        /// <summary>
        /// Button cho phòng ban tổng đài 
        /// </summary>
        Close = 3
    }

    /// <summary>
    /// Lấy Id trong database
    /// </summary>
    public enum VOC_CatType
    {
        /// <summary>
        /// Mã từ chối
        /// </summary>
        DeniedCode = 1,
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        Document = 3,
        /// <summary>
        /// Nhà mạng
        /// </summary>
        Provider = 4,
        /// <summary>
        /// Tình trạng hàng hóa
        /// </summary>
        TinhTrangHangHoa = 19,
        /// <summary>
        /// Loại sản phẩm (Loại nghành hàng)
        /// </summary>
        LoaiSanPham = 15,
        /// <summary>
        /// Hiện trạng đang xử lý(tiếp nhận)
        /// </summary>
        HienTrangXuLy_TiepNhan = 21,
        /// <summary>
        /// Tình trạng hư hỏng(thuộc MP)
        /// </summary>
        TinhTrangHuHong_ThuocMP = 20,

        /// <summary>
        /// Nguồn nhận
        /// </summary>
        NguonNhan = 9,
        /// <summary>
        /// Mục đích
        /// </summary>
        MucDich = 11,
        /// <summary>
        /// Mục đích của MP vận hành
        /// </summary>
        MucDichMP = 1016,
        /// <summary>
        /// Kênh tiếp nhận
        /// </summary>
        KenhTiepNhan = 10,
        /// <summary>
        /// Hướng xử lý sau tư vấn/tab Tư vấn
        /// </summary>
        HuongXuLySauTuVan_TuVan = 22,

        #region Chăm sóc sửa chữa

        HuongXuLySauChamSoc = 1009,
        /// <summary>
        /// Hiện trạng/ chăm sóc sau sửa chữa
        /// </summary>
        HienTrangChamSocSauSuaChua = 1010,
        /// <summary>
        /// Hướng xử lý sau tư vấn/tab Kỹ thuật
        /// </summary>
        HuongXuLySauTuVan_KyThuat = 1011,
        /// <summary>
        /// Loại cửa hàng đại lý
        /// </summary>
        LoaiCuaHangDaiLy = 1012,
        /// <summary>
        /// Các bước chăm sóc
        /// </summary>
        CacBuocChamSoc = 28,
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        LoaiChungTu = 16,
        /// <summary>
        /// Loại chứng từ khác, trong danh mục là Chứng từ không hợp lệ
        /// </summary>
        LoaiChungTuKhac = 18,
        /// <summary>
        /// Hướng xử lý ở trạm
        /// </summary>
        HuongXuLy_Tram = 1013,

        #endregion

        /// <summary>
        /// VOC trạng thái sự vụ
        /// </summary>
        VOCTrangThaiSuVu = 1014,
        /// <summary>
        /// Trạng thái xử lý cuối cùng, trạm xử lý, tab xử lý cuối cùng
        /// </summary>
        VOCTrangThaiXuLyCuoiCung = 27
    }


    public enum CC_CallStatus
    {
        NewChannel = 1,

    }

    public enum CC_QueueDirection
    {
        Outbound = 0,
        Inbound = 1,
        Both = 2
    }
}
