using MP.Common;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VAS.Dealer.Services.Interfaces
{
    public interface ICategoryServices
    {
        /// <summary>
        /// Lấy thông tin hiển thị cây thư mục
        /// </summary>
        /// <returns></returns>
        CategoryModel GetTree();
        /// <summary>
        /// Lấy danh sách category theo catTypeId
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        List<CategoryItemModel> GetCategoryByCatType(int catTypeId);
        /// <summary>
        /// Lấy category theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        object GetCategoryByCode(string code);
        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        void Update(MP_Category model, string userName);
        /// <summary>
        /// Thêm mới danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        void Add(MP_Category model, string userName);
        /// <summary>
        /// Thêm mới loại danh mục
        /// </summary>
        /// <param name="model"></param>
        void AddCatType(MP_CatType model);
        /// <summary>
        /// Xóa danh mục
        /// </summary>
        /// <param name="id"></param>
        void DeleteCategory(int id);
        /// <summary>
        /// Kiểm tra tồn tại mã danh mục
        /// </summary>
        /// <param name="code"></param>
        /// <returns>True: tồn tại, False: không tồn tại</returns>
        bool CheckExistCategoryCode(string code);
        /// <summary>
        /// Lấy ra model cho select2
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        SelectList GetCategoryByCatType(VOC_CatType catTypeId, string selected = null);
        /// <summary>
        /// Lấy danh mục cho mục đích
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        SelectList GetPurposeCategory(string selected = null);
        /// <summary>
        /// Lấy ra model cho select2 multi
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetCategoryByCatType_Multi(VOC_CatType catTypeId, IEnumerable<string> selected = null);
        /// <summary>
        /// Lấy SelectList 
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        SelectList GetCategoryByCatType(VOC_CatType catTypeId);
        /// <summary>
        /// Lấy ra model cho select2 với SelectList nối Code - Name
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        SelectList GetCategoryByCatType_CodeWithName(VOC_CatType catTypeId, string selected = null);
        /// <summary>
        /// Lấy ra model cho select2 với Select2Model với value, Name
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        List<Select2Model> GetCategoryByCatTypeModel_ValueName(VOC_CatType catTypeId);
        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// Không cho chọn những trạng thái không thuộc isManager
        /// Cho chọn trọng thái hiện tại
        /// </summary>
        /// <param name="isManager"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSolutionAfterDiscus(bool isManager = false, string selected = "");
        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// Không cho chọn những trạng thái không thuộc isManager
        /// Cho chọn trọng thái hiện tại
        /// </summary>
        /// <param name="isManager"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSolutionAfterTech(bool isManager = false, string selected = "");
        /// <summary>
        /// Lấy ra model cho select2 name + ExpandProperties
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        SelectList GetCategoryByCatTypeCodeWithNameAndDescription(VOC_CatType catTypeId);
        /// <summary>
        /// Khu vực
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public SelectList GetStoreArea(string selected = null);
        /// <summary>
        /// Khu vực
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Select2Model> GetStoreAreaWithModel(string userName = null, int id = 0);
        /// <summary>
        /// Lấy thông tin tỉnh thành theo khu vực
        /// </summary>
        /// <param name="area"></param>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        List<Select2Model> GetProvinceByArea(string area, string selectedItem);
        /// <summary>
        /// Lấy thông tin tỉnh thành theo khu vực
        /// </summary>
        /// <param name="area"></param>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        SelectList GetSelectListProvinceByArea(string area, string selectedItem);
        /// <summary>
        /// Lấy thông tin huyện theo tỉnh
        /// </summary>
        /// <param name="province"></param>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        List<Select2Model> GetDistrictByProvince(string province, string selectedItem);
        /// <summary>
        /// Lấy thông tin huyện theo tỉnh
        /// </summary>
        /// <param name="province"></param>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        SelectList GetSelectListDistrictByProvince(string province, string selectedItem);
        /// <summary>
        /// Lấy thông tin model theo loại nghành hàng
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="selectedModel"></param>
        /// <returns></returns>
        List<Select2Model> GetModelByProductType(string productType, string selectedModel);
        /// <summary>
        /// Lấy thông tin model theo loại nghành hàng
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="selectedModel"></param>
        /// <returns></returns>
        SelectList GetSelectListModelByProductType(string productType, string selectedModel);
        /// <summary>
        /// Lấy data cho Màu, Dung tích/Công suất khi chọn loại nghành hàng, gán selected color
        /// </summary>
        /// <param name="productModel"></param>
        /// <param name="selectedColor"></param>
        /// <returns></returns>
        ProductDetialByModelModel GetProductDetailByModel(string productModel, string selectedColor);
        /// <summary>
        /// Lấy data cho Màu khi chọn loại nghành hàng, gán selected color
        /// </summary>
        /// <param name="productModel"></param>
        /// <param name="selectedModel"></param>
        /// <returns></returns>
        SelectList GetSelectListColorByModel(string productModel, string selectedModel);
        /// <summary>
        /// Lấy cấu hình email đang sử dụng
        /// </summary>
        /// <returns></returns>
        MailConfigModel GetEmailConfigInUse();
        /// <summary>
        /// Lưu thông tin cấu hình mail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool SaveMailConfig(MailConfigModel model, string userName, out string message);
        /// <summary>
        /// Lưu lại nội dung email
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool SaveMailContent(MailConfigModel model, string userName, out string message);
        /// <summary>
        /// Kiểm tra tồn tại email config
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True: tồn tài, False: không tồn tại</returns>
        bool CheckExistEmailConfig(int id);
        /// <summary>
        /// Lấy cấu hình crm theo mã code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ConfigModel GetConfigByCode(string code);
    }
}
