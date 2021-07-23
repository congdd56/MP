using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MP.Common;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class APIServices : IAPIServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly MP_Context _Context;
        private readonly VINAServiceModel _VINAServiceModel;
        private readonly IEmailServices _EmailServices;

        public APIServices(MP_Context CrmContext, ILogger<UserServices> logger, IOptions<VINAServiceModel> VINAServiceModel,
            IEmailServices EmailServices)
        {
            _Context = CrmContext;
            _logger = logger;
            _EmailServices = EmailServices;
            _VINAServiceModel = VINAServiceModel.Value;
        }

        /// <summary>
        /// Lấy session đăng nhập
        /// </summary>
        /// <returns></returns>
        async Task<LoginResponseModel> Login()
        {
            try
            {
                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.Login, "application/json");
                var response = await client.ExecuteAsync<LoginResponseModel>(request);

                _logger.LogError($"Result Login : {JsonConvert.SerializeObject(response.Data)}");

                if (response.Data != null && response.Data.ErrorCode == "0")
                    return response.Data;

            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError($"{ex.Message}, user: {_VINAServiceModel.Login.username}/{_VINAServiceModel.Login.password}", ex);
            }
            return null;
        }

        /// <summary>
        /// Danh sách  dịch vụ theo mã
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public async Task<LayDanhSachGoiDichVuResponseModel> LayDanhSachGoiDichVu(string services = "")
        {
            try
            {
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.LayDanhSachGoiDichVu.session = session.Session;

                _VINAServiceModel.LayDanhSachGoiDichVu.p_service_code = services;
                _logger.LogError($"Data LayDanhSachGoiDichVu: {JsonConvert.SerializeObject(_VINAServiceModel.LayDanhSachGoiDichVu)}");

                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.LayDanhSachGoiDichVu, "application/json");
                var response = await client.ExecuteAsync<LayDanhSachGoiDichVuResponseModel>(request);

                _logger.LogError($"Login LayDanhSachGoiDichVu: {JsonConvert.SerializeObject(response.Data)}");

                return response.Data;
            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Lay tất cả dịch vụ
        /// </summary>
        /// <returns></returns>
        public async Task<LayDanhSachGoiDichVuResponseModel> layTatCaGoiDichVu()
        {
            try
            {
                _logger.LogError($"Data LayDanhSachGoiDichVu: {JsonConvert.SerializeObject(_VINAServiceModel.LayDanhSachGoiDichVu)}");

                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.LayDanhSachGoiDichVu.session = session.Session;

                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.LayDanhSachGoiDichVu, "application/json");
                var response = await client.ExecuteAsync<LayDanhSachGoiDichVuResponseModel>(request);

                _logger.LogError($"Login LayDanhSachGoiDichVu: {JsonConvert.SerializeObject(response.Data)}");

                return response.Data;
            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Tạo đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> TaoDaiLyCon(TaoDaiLyConRequestModel model)
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.TaoDaiLyCon.session = session.Session;

                _VINAServiceModel.TaoDaiLyCon.branch_code = model.branch_code;
                _VINAServiceModel.TaoDaiLyCon.branch_name = model.branch_name;
                _VINAServiceModel.TaoDaiLyCon.branch_mobile = model.branch_mobile;

                _logger.LogError($"Data TaoDaiLyCon: {JsonConvert.SerializeObject(_VINAServiceModel.TaoDaiLyCon)}");
                #endregion


                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.TaoDaiLyCon, "application/json");
                var response = await client.ExecuteAsync<APIResponseModel>(request);

                _logger.LogError($"Result TaoDaiLyCon: {JsonConvert.SerializeObject(response.Data)}");

                //Gắn lại mã giao dịch
                if (response != null) response.Data.TradeKey = session.TradeKey;

                return response.Data;


            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Tạo đại lý con
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<TimKiemDaiLyResponseModel> TimKiemDaiLy(string code = "")
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.TimKiemDaiLy.session = session.Session;

                _VINAServiceModel.TimKiemDaiLy.p_code = code;

                _logger.LogError($"Data TaoDaiLyCon: {JsonConvert.SerializeObject(_VINAServiceModel.TimKiemDaiLy)}");
                #endregion


                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.TimKiemDaiLy, "application/json");
                var response = await client.ExecuteAsync<TimKiemDaiLyResponseModel>(request);

                _logger.LogError($"Result TaoDaiLyCon: {JsonConvert.SerializeObject(response.Data)}");

                return response.Data;
            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Đăng ký mới thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> DaiLyMoiGoi(DaiLyMoiGoiRequestModel model)
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.DaiLyMoiGoi.session = session.Session;

                _VINAServiceModel.DaiLyMoiGoi.service_code = model.Service;
                _VINAServiceModel.DaiLyMoiGoi.branch_code = model.Branch;
                _VINAServiceModel.DaiLyMoiGoi.so_tb = model.PhoneNumber;

                _logger.LogError($"Data TaoDaiLyCon: {JsonConvert.SerializeObject(_VINAServiceModel.DaiLyMoiGoi)}");
                #endregion


                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.DaiLyMoiGoi, "application/json");
                var response = await client.ExecuteAsync<APIResponseModel>(request);

                _logger.LogError($"Result TaoDaiLyCon: {JsonConvert.SerializeObject(response.Data)}");

                //Gắn lại mã giao dịch
                if (response != null) response.Data.TradeKey = session.TradeKey;

                return response.Data;

            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }


        /// <summary>
        /// Sửa thông tin đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> SuaThongTinDaiLy(TaoDaiLyConRequestModel model)
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.SuaThongTinDaiLy.session = session.Session;

                _VINAServiceModel.SuaThongTinDaiLy.branch_code = model.branch_code;
                _VINAServiceModel.SuaThongTinDaiLy.branch_name = model.branch_name;
                _VINAServiceModel.SuaThongTinDaiLy.branch_mobile = model.branch_mobile;

                _logger.LogError($"Data SuaThongTinDaiLy: {JsonConvert.SerializeObject(_VINAServiceModel.SuaThongTinDaiLy)}");
                #endregion

                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.SuaThongTinDaiLy, "application/json");
                var response = await client.ExecuteAsync<APIResponseModel>(request);

                _logger.LogError($"Result SuaThongTinDaiLy: {JsonConvert.SerializeObject(response.Data)}");

                //Gắn lại mã giao dịch
                if (response != null) response.Data.TradeKey = session.TradeKey;

                return response.Data;

            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Xóa đại lý con
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> XoaDaiLy(XoaDaiLyRequestModel model)
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.XoaDaiLy.session = session.Session;

                _VINAServiceModel.XoaDaiLy.branch_code = model.BranchCode;
                _VINAServiceModel.XoaDaiLy.description = model.Reasion;

                _logger.LogError($"Data XoaDaiLy: {JsonConvert.SerializeObject(_VINAServiceModel.XoaDaiLy)}");
                #endregion

                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.XoaDaiLy, "application/json");
                var response = await client.ExecuteAsync<APIResponseModel>(request);

                _logger.LogError($"Result XoaDaiLy: {JsonConvert.SerializeObject(response.Data)}");

                //Gắn lại mã giao dịch
                if (response != null) response.Data.TradeKey = session.TradeKey;

                return response.Data;

            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Gia hạn thuê bao
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> DaiLyMoiGiaHan(DaiLyMoiGiaHanRequestModel model)
        {
            try
            {
                #region Chuẩn bị data
                var session = await Login();
                if (session == null) return null;

                _VINAServiceModel.DaiLyMoiGiaHan.session = session.Session;

                _VINAServiceModel.DaiLyMoiGiaHan.branch_code = model.Branch;
                _VINAServiceModel.DaiLyMoiGiaHan.service_code = model.Service;
                _VINAServiceModel.DaiLyMoiGiaHan.so_tb = model.PhoneNumber;

                _logger.LogError($"Data DaiLyMoiGiaHan: {JsonConvert.SerializeObject(_VINAServiceModel.DaiLyMoiGiaHan)}");
                #endregion

                var client = new RestClient(_VINAServiceModel.Uri)
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(_VINAServiceModel.DaiLyMoiGiaHan, "application/json");
                var response = await client.ExecuteAsync<APIResponseModel>(request);

                _logger.LogError($"Result DaiLyMoiGiaHan: {JsonConvert.SerializeObject(response.Data)}");

                //Gắn lại mã giao dịch
                if (response != null) response.Data.TradeKey = session.TradeKey;

                return response.Data;

            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }
            return null;
        }
    }
}
