
#region IMPLEMENT

using ClosedXML.Report;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MP.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;
using VAS.Dealer.Services.Interfaces;

#endregion

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class VASController : MPBaseController
    {

        #region PROPERTIES

        private readonly IConfiguration _Configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<VASController> _logger;
        private readonly IUserServices _userServices;
        private readonly IVASServices _VASServices;
        private readonly IAPIServices _APIServices;
        private readonly ILottoServices _LottoServices;

        #endregion


        #region CONTRUCTOR
        public VASController(ILogger<VASController> logger, IUserServices userServices, IVASServices VASServices,
            IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IAPIServices APIServices,ILottoServices LottoServices)
        {
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _userServices = userServices;
            _VASServices = VASServices;
            _APIServices = APIServices;
            _LottoServices = LottoServices;
        }

        #endregion
        /// <summary>
        /// Tra cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult HisRegis()
        {
            return View();
        }

        [Route("/VAS/HisRegis")]
        [HttpPost]
        public async Task<IActionResult> HisRegis([FromBody] PagingAllHisRegisRequestModel model)
        { 
            return Ok();
        }
        /// <summary>
        /// Xuất excel Báo cáo Đăng ký
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("VAS/ExportHisRegis")] 
        public async Task<IActionResult> ExportWarrantyArise([FromBody] PagingAllHisRegisRequestModel model)
        {
            model.IsExport = 1;
            #region VALIDATE
            //var checkFDate = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture,
            //          DateTimeStyles.None, out DateTime FDate);
            //var checkTDate = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture,
            //         DateTimeStyles.None, out DateTime TDate);
            //if (!checkFDate || !checkTDate)
            //{
            //    return BadRequest("Sai định dạng thời gian tìm kiếm");
            //}
             
            #endregion

            #region HANDLER

            try
            {
                var contentRoot = _hostingEnvironment.ContentRootPath;
                string fileName = $"ExportHisRegis - {DateTime.Now:yyyyHHmmddMMhhss} - {UserLogon.UserName}.xlsx";
                string path = $"\\Export\\File\\{fileName}";
                string outputFile = contentRoot + path;
                var template = new XLTemplate($"{contentRoot}\\Export\\Template\\Report\\ExportHisRegis.xlsx");

                var listRegister = await _VASServices.GetRegister(model);

                HisRegisterExportModel Result = new HisRegisterExportModel()
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Details = listRegister.data
                };

                template.AddVariable(Result);
                template.Generate();
                template.SaveAs(outputFile);

                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileName,
                    Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                return File(System.IO.File.ReadAllBytes(outputFile), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return BadRequest("Không thể tạo file excel. " + ex.Message);
            }

            #endregion

        }

        /// <summary>
        /// Danh sách sự vụ
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Service()
        {

            return View(await _APIServices.LayDanhSachGoiDichVu());
        }

        #region Quản lý đại lý
        /// <summary>
        /// Màn hình
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Vendor()
        {
            return View(await _VASServices.ListVendor());
        }

        /// <summary>
        /// Danh sách đại lý
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/VAS/ListVendor")]
        public async Task<IActionResult> ListVendor()
        {
            return Ok(await _VASServices.GetAllVendor());
        }


        /// <summary>
        /// Tạo mới đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [MPAuthorize]
        [HttpPost]
        [Route("/VAS/CreateVendor")]
        public async Task<IActionResult> CreateVendor([FromBody] TaoDaiLyConRequestModel model)
        {
            if (!_VASServices.ValidateCreateVendor(model, out string message))
                return BadRequest(message);

            var result = await _VASServices.CreateVendor(model, UserLogon.UserName);
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok();
        }

        /// <summary>
        /// Lấy thông tin đại lý theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [MPAuthorize]
        [HttpGet]
        [Route("/VAS/VendorInfo/{code}")]
        public async Task<IActionResult> VendorInfo(string code)
        {
            return Ok(await _VASServices.GetVendorByCode(code));
        }

        /// <summary>
        /// Cập nhật đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [MPAuthorize]
        [HttpPost]
        [Route("/VAS/UpdateVendor")]
        public async Task<IActionResult> UpdateVendor([FromBody] TaoDaiLyConRequestModel model)
        {
            if (!_VASServices.ValidateUpdateVendor(model, out string message))
                return BadRequest(message);

            var result = await _VASServices.UpdateVendor(model, UserLogon.UserName);
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok();
        }

        /// <summary>
        /// Xóa đại lý
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [MPAuthorize]
        [HttpPost]
        [Route("/VAS/DeleteVendor")]
        public async Task<IActionResult> DeleteVendor([FromBody] XoaDaiLyRequestModel model)
        {
            if (!_VASServices.ValidateDeleteVendor(model.BranchCode, out string message))
                return BadRequest(message);

            var result = await _VASServices.DeleteVendor(model, UserLogon.UserName);
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok();
        }

        #endregion

        #region Dịch vụ
        [HttpGet]
        [Route("/VAS/LayDanhSachGoiDichVu")]
        public async Task<IActionResult> LayDanhSachGoiDichVu()
        {
            List<Select2Model> model = new List<Select2Model>();
            var services = await _APIServices.LayDanhSachGoiDichVu();
            if (services != null && services.Result != null && services.Result.Count > 0)
                model = services.Result.Select(s => new Models.CRM.Select2Model()
                {
                    id = s.DVU,
                    text = s.DVU,
                    des = s.MT
                }).ToList();

            return Ok(model);
        }
        #endregion


        #region Đại lý mời gói
        /// <summary>
        /// Màn hình
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register()
        {
            return View();
        }

        /// <summary>
        /// Mời gói
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("VAS/RegisterPhone")]
        public async Task<IActionResult> RegisterPhone([FromBody] DaiLyMoiGoiRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.Branch) || string.IsNullOrEmpty(model.Service) || string.IsNullOrEmpty(model.PhoneNumber))
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            #endregion

            #region HANDLER

            var result = await _APIServices.DaiLyMoiGoi(model);
            if (result == null || result.ErrorCode != "0")
            {
                string message = result == null ? "Lỗi gọi API nhà mạng" : result.Message;
                return BadRequest($"Đăng ký thuê bao không thành công. {message}");
            }

            await _VASServices.SaveRegis(model, result.TradeKey, UserLogon.UserName);
            return Ok();
            #endregion
        }


        /// <summary>
        /// Gia hạn gói
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("VAS/ExtendPhone")]
        public async Task<IActionResult> ExtendPhone([FromBody] DaiLyMoiGiaHanRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.Branch) || string.IsNullOrEmpty(model.Service) || string.IsNullOrEmpty(model.PhoneNumber))
                return BadRequest("Vui lòng nhập đầy đủ thông tin");
            #endregion

            #region HANDLER

            var result = await _APIServices.DaiLyMoiGiaHan(model);
            if (result.ErrorCode == "0")
            {
                await _VASServices.SaveExtend(model, result.TradeKey, UserLogon.UserName);
                return Ok();
            }

            return BadRequest($"Gia hạn thuê bao không thành công. {result.Message}");

            #endregion
        }

        /// <summary>
        /// Lịch sử đăng ký mới thuê bao theo tài khoản
        /// </summary>
        /// <returns></returns>
        [MPAuthorize(Permission = MPConst.vas_register)]
        [HttpPost]
        [Route("VAS/RegisteredHistory")]
        public async Task<IActionResult> RegisteredHistory([FromBody] PagingRequestModel model)
        {
            return Ok(await _VASServices.GetRegisterByUser(UserLogon.UserName, model));
        }
        #endregion

        //xu ly Lotto 
        [HttpPost]

        [Route("/VAS/UpdateLotto")]
        public object UpdateLotto()
        { 
            //check quyen admin 
            if (UserLogon.UserName == "admin")
            {
                return Ok("err-exitsaccount");
            }
            var accountId = UserLogon.UserId;
            if (_LottoServices.checkIsClosed(accountId))
                return Ok("err-exitlotto");

            var result = _LottoServices.UpdateLotto(accountId);
            return new { value = result };
        }

        //xu ly Lotto 
        [HttpPost]

        [Route("/VAS/GetListLotto")]
        public object GetListLotto()
        {
            var result = _LottoServices.GetListLotto();
            return new { value = result };
        }

    }
}