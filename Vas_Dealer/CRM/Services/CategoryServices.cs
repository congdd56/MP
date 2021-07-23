using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly MP_Context _Context;
        private readonly ILogger<CategoryServices> _logger;
        public CategoryServices(MP_Context Context, ILogger<CategoryServices> logger)
        {
            _Context = Context;
            _logger = logger;
        }
        /// <summary>
        /// Lấy thông tin hiển thị cây thư mục
        /// </summary>
        /// <returns></returns>
        public CategoryModel GetTree()
        {
            List<CategoryModel> lst = _Context.CatType.Where(x => !x.IsDeleted).Select(s => new CategoryModel()
            {
                CatTypeId = s.Id,
                CatTypeGroup = s.Group,
                CatTypeName = s.Name,
                HasChild = false,
                text = s.Name,
                icon = "fa fa-flag-checkered text-info"
            }).ToList();
            return new CategoryModel()
            {
                text = "Danh mục",
                children = lst,
                state = new StateCategoryTree()
                {
                    opened = true,
                    selected = true
                }
            };
        }

        /// <summary>
        /// Lấy danh sách category theo catTypeId
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        public List<CategoryItemModel> GetCategoryByCatType(int catTypeId)
        {
            var item = _Context.Category.Where(x => x.CatTypeId == catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new CategoryItemModel()
                {
                    Code = s.Code,
                    Name = s.Name,
                    Id = s.Id,
                    Value = s.Value,
                    ExpandProperties = s.ExpandProperties
                }).ToList();
            return item;
        }

        /// <summary>
        /// Cập nhật danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        public void Update(MP_Category model, string userName)
        {
            var item = _Context.Category.Where(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();
            if (item != null)
            {
                item.Code = model.Code;
                item.Name = model.Name;
                item.ExpandProperties = model.ExpandProperties;
                item.UpdatedBy = userName;
                item.UpdatedDate = DateTime.Now;
                _Context.SaveChanges();
            }
        }
        /// <summary>
        /// Lấy category theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public object GetCategoryByCode(string code)
        {
            return _Context.Category.Where(x => !x.IsDeleted && x.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// Tạo mới danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        public void Add(MP_Category model, string userName)
        {
            _Context.Category.Add(new MP_Category()
            {
                CatTypeId = model.CatTypeId,
                Code = model.Code,
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Name = model.Name,
                ExpandProperties = model.ExpandProperties
            });
            _Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới loại danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddCatType(MP_CatType model)
        {
            _Context.CatType.Add(new MP_CatType()
            {
                IsDeleted = false,
                Name = model.Name,
                Group = model.Group
            });
            _Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra tồn tại mã danh mục
        /// </summary>
        /// <param name="code"></param>
        /// <returns>True: tồn tại, False: không tồn tại</returns>
        public bool CheckExistCategoryCode(string code)
        {
            return _Context.Category.Any(x => x.Code == code && !x.IsDeleted);
        }

        /// <summary>
        /// Xóa danh mục
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            var item = _Context.Category.Where(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {
                item.IsDeleted = true;
                _Context.SaveChanges();
            }
        }

        #region model

        /// <summary>
        /// Lấy ra model cho select2 với Select2Model với value, Name
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        public List<Select2Model> GetCategoryByCatTypeModel_ValueName(VOC_CatType catTypeId)
        {
            return _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new Select2Model
                {
                    text = $"{s.Name}",
                    id = s.Value
                }).ToList();
        }
        public SelectList GetCategoryByCatType_CodeWithName(VOC_CatType catTypeId, string selected = null)
        {
            var item = _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Code} - {s.Name}",
                    Value = s.Code
                }).ToList();
            return new SelectList(item, "Value", "Text", selectedValue: selected);
        }
        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// Không cho chọn những trạng thái không thuộc isManager
        /// Cho chọn trọng thái hiện tại
        /// </summary>
        /// <param name="isManager"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSolutionAfterDiscus(bool isManager = false, string selected = "")
        {
            var valCheck = isManager ? "1" : "0";
            var item = _Context.Category.Where(x => x.CatTypeId == (int)VOC_CatType.HuongXuLySauTuVan_TuVan && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Code}",
                    Value = s.Code,
                    Disabled = (s.Value != valCheck && s.Code != selected && !string.IsNullOrEmpty(s.Value)),
                    Selected = s.Code == selected
                }).ToList();
            return item;
        }
        /// <summary>
        /// Hướng xử lý sau tư vấn
        /// Không cho chọn những trạng thái không thuộc isManager
        /// Cho chọn trọng thái hiện tại
        /// </summary>
        /// <param name="isManager"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSolutionAfterTech(bool isManager = false, string selected = "")
        {
            var valCheck = isManager ? "1" : "0";
            var item = _Context.Category.Where(x => x.CatTypeId == (int)VOC_CatType.HuongXuLySauTuVan_KyThuat && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Name}",
                    Value = s.Code,
                    Disabled = (s.Value != valCheck && s.Code != selected && !string.IsNullOrEmpty(s.Value)),
                    Selected = s.Code == selected
                }).ToList();
            return item;
        }
        /// <summary>
        /// Lấy ra model cho select2 name + ExpandProperties
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        public SelectList GetCategoryByCatTypeCodeWithNameAndDescription(VOC_CatType catTypeId)
        {
            var item = _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Name} - {s.ExpandProperties}",
                    Value = s.Code
                }).ToList();
            return new SelectList(item, "Value", "Text");
        }
        /// <summary>
        /// Lấy SelectList 
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <returns></returns>
        public SelectList GetCategoryByCatType(VOC_CatType catTypeId)
        {
            var item = _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Code
                }).ToList();
            return new SelectList(item, "Value", "Text");
        }

        /// <summary>
        /// Lấy SelectList có selected
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public SelectList GetCategoryByCatType(VOC_CatType catTypeId, string selected = null)
        {
            var item = _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
                .OrderBy(o => o.OrderBy)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Code
                }).ToList();
            return new SelectList(item, "Value", "Text", selectedValue: selected);
        }

        /// <summary>
        /// Lấy ra model cho select2 multi
        /// </summary>
        /// <param name="catTypeId"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetCategoryByCatType_Multi(VOC_CatType catTypeId, IEnumerable<string> selected = null)
        {
            if (selected == null) selected = new List<string>();
            return _Context.Category.Where(x => x.CatTypeId == (int)catTypeId && !x.IsDeleted)
               .OrderBy(o => o.OrderBy)
               .Select(s => new SelectListItem
               {
                   Text = s.Name,
                   Value = s.Code,
                   Selected = selected.Contains(s.Code)
               }).ToList();
        }
        #endregion


        #region Email config
        /// <summary>
        /// Lấy cấu hình email đang sử dụng
        /// </summary>
        /// <returns></returns>
        public MailConfigModel GetEmailConfigInUse()
        {
            return _Context.EmailConfig.Where(x => !x.IsDeleted).Select(s => new MailConfigModel()
            {
                CreatedBy = s.CreatedBy,
                Host = s.Host,
                Id = s.Id,
                Password = s.Password,
                Port = s.Port,
                PortServer = s.PortServer,
                Encode = true,
                Server = s.Server,
                UserName = s.UserName,
                SSLoTLS = s.SSLoTLS,
                MailSubject = s.MailSubject,
                MailContent = s.MailContent
            }).FirstOrDefault();
        }

        /// <summary>
        /// Lưu thông tin cấu hình mail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveMailConfig(MailConfigModel model, string userName, out string message)
        {
            message = string.Empty;
            try
            {
                //Cập nhật bản ghi = isdeleted
                var itemDelete = _Context.EmailConfig.Where(x => !x.IsDeleted).ToList();
                if (itemDelete != null && itemDelete.Count > 0)
                {
                    itemDelete.ForEach(x =>
                    {
                        x.IsDeleted = true;
                    });
                }

                _Context.EmailConfig.Add(new MP_EmailConfig()
                {
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Host = model.Host,
                    IsDeleted = false,
                    Password = !model.Encode ? MPHelper.Encrypt(model.Password) : model.Password,
                    Port = model.Port,
                    PortServer = model.PortServer,
                    Server = model.Server,
                    SSLoTLS = model.SSLoTLS,
                    UserName = model.UserName,
                    MailContent = itemDelete[0].MailContent,
                    MailSubject = itemDelete[0].MailSubject,
                });
                _Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                message = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// Lưu lại nội dung email
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveMailContent(MailConfigModel model, string userName, out string message)
        {
            message = string.Empty;
            try
            {
                var item = _Context.EmailConfig.Where(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();
                if (item != null)
                {
                    item.MailContent = model.MailContent;
                    item.MailSubject = model.MailSubject;
                    item.UpdatedBy = userName;
                    item.UpdatedDate = DateTime.Now;
                    _Context.SaveChanges();
                    return true;
                }
                else
                {
                    message = "Đừng yêu em nữa";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                message = ex.Message;
                return false;
            }


        }

        /// <summary>
        /// Lấy danh mục cho mục đích
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public SelectList GetPurposeCategory(string selected = null)
        {
            var itemDPL = _Context.Category.Where(x => x.CatTypeId == (int)VOC_CatType.MucDich && !x.IsDeleted)
              .Select(s => new SelectListItem
              {
                  Group = new SelectListGroup()
                  {
                      Name = "DPL"
                  },
                  Text = s.Name,
                  Value = s.Code,
                  Selected = s.Code == selected
              }).ToList();

            var itemMP = _Context.Category.Where(x => x.CatTypeId == (int)VOC_CatType.MucDichMP && !x.IsDeleted)
              .Select(s => new SelectListItem
              {
                  Group = new SelectListGroup()
                  {
                      Name = "MP"
                  },
                  Text = s.Name,
                  Value = s.Code,
                  Selected = s.Code == selected
              }).ToList();

            itemDPL.AddRange(itemMP);

            return new SelectList(itemDPL, "Value", "Text", selectedValue: selected);
        }

        /// <summary>
        /// Kiểm tra tồn tại email config
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True: tồn tài, False: không tồn tại</returns>
        public bool CheckExistEmailConfig(int id)
        {
            return _Context.EmailConfig.Any(x => !x.IsDeleted && x.Id == id);
        }

        public SelectList GetStoreArea(string selected = null)
        {
            throw new NotImplementedException();
        }

        public List<Select2Model> GetStoreAreaWithModel(string userName = null, int id = 0)
        {
            throw new NotImplementedException();
        }

        public List<Select2Model> GetProvinceByArea(string area, string selectedItem)
        {
            throw new NotImplementedException();
        }

        public SelectList GetSelectListProvinceByArea(string area, string selectedItem)
        {
            throw new NotImplementedException();
        }

        public List<Select2Model> GetDistrictByProvince(string province, string selectedItem)
        {
            throw new NotImplementedException();
        }

        public SelectList GetSelectListDistrictByProvince(string province, string selectedItem)
        {
            throw new NotImplementedException();
        }

        public List<Select2Model> GetModelByProductType(string productType, string selectedModel)
        {
            throw new NotImplementedException();
        }

        public SelectList GetSelectListModelByProductType(string productType, string selectedModel)
        {
            throw new NotImplementedException();
        }

        public ProductDetialByModelModel GetProductDetailByModel(string productModel, string selectedColor)
        {
            throw new NotImplementedException();
        }

        public SelectList GetSelectListColorByModel(string productModel, string selectedModel)
        {
            throw new NotImplementedException();
        }

        public ConfigModel GetConfigByCode(string code)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
