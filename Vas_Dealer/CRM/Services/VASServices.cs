using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class VASServices : IVASServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly MP_Context _Context;
        private readonly IAPIServices _APIServices;

        public VASServices(MP_Context CrmContext, ILogger<UserServices> logger, IAPIServices APIServices)
        {
            _Context = CrmContext;
            _logger = logger;
            _APIServices = APIServices;
        }


        /// <summary>
        /// Lịch sử đăng ký mới thuê bao theo tài khoản
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagingResultModel> GetRegisterByUser(string username, PagingRequestModel page)
        {
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.VAS_GetRegisterByUser }] '{username}', {page.start}, {page.length}, N'{page.search.value}'";
                var result = await _Context.VAS_GetRegisterByUser.FromSqlRaw(sql).ToListAsync();
                var counter = page.start;
                result.ForEach(x =>
                {
                    counter++;
                    x.STT = counter;
                });

                return new PagingResultModel()
                {
                    data = result,
                    recordsFiltered = (result != null && result.Count > 0) ? result[0].TotalRows : 0
                };

            }
        }

        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagingResultModel2> GetRegister(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.VAS_GetRegister }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','{page.IsExport}'";
                var result = await _Context.VAS_GetRegister.FromSqlRaw(sql).ToListAsync();
                var counter = page.start;
                result.ForEach(x =>
                {
                    counter++;
                    x.STT = counter;
                });

                return new PagingResultModel2()
                {
                    data = result,
                    recordsFiltered = (result != null && result.Count > 0) ? result[0].TotalRows : 0
                };
            }
        }


        #region Đai lý
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ValidateCreateVendor(TaoDaiLyConRequestModel model, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(model.branch_name) || string.IsNullOrEmpty(model.branch_mobile))
            {
                message = "Vui lòng nhập đầy đủ thông tin";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ValidateUpdateVendor(TaoDaiLyConRequestModel model, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(model.branch_code))
            {
                message = "Đừng yêu em nữa";
                return false;
            }

            if (!_Context.Vendor.Any(x => x.Branch_code == model.branch_code))
            {
                message = "Không tìm thấy thông tin đại lý";
                return false;
            }


            if (string.IsNullOrEmpty(model.branch_name) || string.IsNullOrEmpty(model.branch_mobile))
            {
                message = "Vui lòng nhập đầy đủ thông tin";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ValidateDeleteVendor(string code, out string message)
        {
            message = string.Empty;
            if (!_Context.Vendor.Any(x => x.Branch_code == code))
            {
                message = "Không tìm thấy thông tin đại lý";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tạo mã mới 
        /// </summary>
        /// <returns></returns>
        async Task<string> GenBranchCode()
        {
            var counter = await _Context.Vendor.CountAsync();
            return $"MPViaVAS{(counter + 1).ToString("00000")}";
        }

        /// <summary>
        /// Lưu thông tin đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> CreateVendor(TaoDaiLyConRequestModel model, string userName)
        {
            model.branch_code = await GenBranchCode();
            var result = await _APIServices.TaoDaiLyCon(model);
            if (result == null || result.ErrorCode != "0")
            {
                string message = result == null ? "Lỗi gọi API nhà mạng" : result.Message;
                return $"Tạo dữ liệu không thành công. {message}";
            }

            await SaveVendor(model, userName);
            return string.Empty;
        }

        /// <summary>
        /// Lưu thông tin đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> UpdateVendor(TaoDaiLyConRequestModel model, string userName)
        {
            var result = await _APIServices.SuaThongTinDaiLy(model);
            if (result == null || result.ErrorCode != "0")
            {
                string message = result == null ? "Lỗi gọi API nhà mạng" : result.Message;
                return $"Cập nhật đại lý không thành công. {message}";
            }

            await UpdateDBVendor(model, userName);
            return string.Empty;
        }

        /// <summary>
        /// Delete đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> DeleteVendor(XoaDaiLyRequestModel model, string userName)
        {
            var result = await _APIServices.XoaDaiLy(model);
            if (result == null || result.ErrorCode != "0")
            {
                string message = result == null ? "Lỗi gọi API nhà mạng" : result.Message;
                return $"Xóa đại lý không thành công. {message}";
            }

            await DeleteVendor(model.BranchCode, model.Reasion, userName);
            return string.Empty;
        }

        /// <summary>
        /// Delete db đại lý
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userName"></param>
        /// <param name="reasion"></param>
        /// <returns></returns>
        async Task DeleteVendor(string code, string reasion, string userName)
        {
            var item = await _Context.Vendor.Where(x => x.Branch_code == code).FirstOrDefaultAsync();
            if (item != null)
            {
                item.IsDeleted = true;
                item.UpdatedBy = userName;
                item.UpdatedDate = DateTime.Now;
                item.DeleteReasion = reasion;
                await _Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Lưu DB đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        async Task SaveVendor(TaoDaiLyConRequestModel model, string userName)
        {
            await _Context.Vendor.AddAsync(new VAS_Vendor()
            {
                Branch_code = model.branch_code,
                Branch_mobile = model.branch_mobile,
                Branch_name = model.branch_name,
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                Branch_sales_code = model.branch_sales_code,
                IsDeleted = false
            });
            await _Context.SaveChangesAsync();
        }

        /// <summary>
        /// Lưu cập nhật DB đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        async Task UpdateDBVendor(TaoDaiLyConRequestModel model, string userName)
        {
            var item = await _Context.Vendor.Where(x => x.Branch_code == model.branch_code).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Branch_code = model.branch_code;
                item.Branch_mobile = model.branch_mobile;
                item.Branch_name = model.branch_name;
                item.UpdatedBy = userName;
                item.UpdatedDate = DateTime.Now;
            }
            await _Context.SaveChangesAsync();
        }



        /// <summary>
        /// Danh sách đại lý đã tạo
        /// </summary>
        /// <returns></returns>
        public async Task<List<VendorResponseModel>> ListVendor()
        {
            var item = await _Context.Vendor.Where(x => !x.IsDeleted).Select(s => new VendorResponseModel()
            {
                Code = s.Branch_code,
                Name = s.Branch_name,
                Phone = s.Branch_mobile,
                CreatedDate = s.CreatedDate,
                CreatedBy = s.CreatedBy
            }).ToListAsync();

            var counter = 1;
            item.ForEach(x =>
            {
                x.STT += counter;
                counter++;
            });

            return item;
        }

        #endregion

        /// <summary>
        /// Lấy đại lý theo quyền
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="allowAll"></param>
        /// <returns></returns>
        public async Task<List<Select2Model>> GetVendorByPermis(string userName, bool allowAll = false)
        {
            if (allowAll)
                return await _Context.Vendor.Where(x => !x.IsDeleted).Select(s => new Select2Model()
                {
                    id = s.Branch_code,
                    text = $"{s.Branch_code}-{s.Branch_name}"
                }).ToListAsync();
            else
                return await _Context.Vendor.Where(x => !x.IsDeleted && x.Account.UserName.Equals(userName))
                    .Select(s => new Select2Model()
                    {
                        id = s.Branch_code,
                        text = $"{s.Branch_code}-{s.Branch_name}"
                    }).ToListAsync();
        }

        /// <summary>
        /// Lấy đại lý
        /// </summary>
        /// <returns></returns>
        public async Task<List<Select2Model>> GetAllVendor()
        {
            return await _Context.Vendor.Where(x => !x.IsDeleted).Select(s => new Select2Model()
            {
                id = s.Id.ToString(),
                text = $"{s.Branch_code}-{s.Branch_name}"
            }).ToListAsync();
        }

        /// <summary>
        /// Lưu thông tin đăng ký
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="tradeKey"></param>
        /// <returns></returns>
        public async Task SaveRegis(DaiLyMoiGoiRequestModel model, string tradeKey, string userName)
        {
            await _Context.Registered.AddAsync(new VAS_Registered()
            {
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                Phone = model.PhoneNumber,
                Type = "Mời gói",
                TradeKey = tradeKey,
                Services = model.Service,
                Vendor = model.Branch
            });
            await _Context.SaveChangesAsync();
        }

        /// <summary>
        /// Gia hạn thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="tradeKey"></param>
        /// <returns></returns>
        public async Task SaveExtend(DaiLyMoiGiaHanRequestModel model, string tradeKey, string userName)
        {
            await _Context.Registered.AddAsync(new VAS_Registered()
            {
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                Phone = model.PhoneNumber,
                Type = "Gia hạn",
                TradeKey = tradeKey,
                Services = model.Service,
                Vendor = model.Branch
            });
            await _Context.SaveChangesAsync();
        }

        /// <summary>
        /// Lấy thông tin đại lý theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<VendorResponseModel> GetVendorByCode(string code)
        {
            return await _Context.Vendor.Where(x => x.Branch_code == code && !x.IsDeleted)
                .Select(s => new VendorResponseModel()
                {
                    CreatedBy = s.CreatedBy,
                    CreatedDate = DateTime.Now,
                    Code = s.Branch_code,
                    Name = s.Branch_name,
                    Phone = s.Branch_mobile
                }).FirstOrDefaultAsync();
        }

    }
}
