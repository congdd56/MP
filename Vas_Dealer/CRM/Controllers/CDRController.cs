
#region IMPLEMENT

using ClosedXML.Report;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MP.Common;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Authentication;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Models.VAS.VINAService;
using VAS.Dealer.Services.Interfaces;

#endregion

namespace VAS.Dealer.Controllers
{
    [MPAuthorize]
    public class CDRController : MPBaseController
    {

        #region PROPERTIES

        private readonly IConfiguration _Configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<CDRController> _logger;
        private readonly IUserServices _userServices;
        private readonly ICDRServices _CDRServices;
        private readonly IAPIServices _APIServices;
        private readonly IFTPServices _FTPServices;
        private readonly FTPConfigModel _FTPConfigModel;
        private readonly IEmailServices _EmailServices;

        #endregion


        #region CONTRUCTOR
        public CDRController(ILogger<CDRController> logger, IUserServices userServices, ICDRServices CDRServices,
            IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IAPIServices APIServices,
            IFTPServices FTPServices, IOptions<FTPConfigModel> FTPConfig, IEmailServices EmailServices)
        {
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _userServices = userServices;
            _CDRServices = CDRServices;
            _APIServices = APIServices;
            _FTPServices = FTPServices;
            _FTPConfigModel = FTPConfig.Value;
            _EmailServices = EmailServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// PrefixRegis1Day
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/CDR/PrefixRegis1Day")]
        [HttpPost]
        public async Task<IActionResult> PrefixRegis1Day([FromBody] PagingAllHisRegisRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            #endregion


            await _FTPServices.InitDataRegis1Day(fromDate, toDate, UserLogon.UserName);

            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            return Ok(await _CDRServices.PrefixRegis1Day(model));
        }


        /// <summary>
        /// PrefixRegis30
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/CDR/PrefixRegis30")]
        [HttpPost]
        public async Task<IActionResult> PrefixRegis30([FromBody] PagingAllHisRegisRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            #endregion
             
            await _FTPServices.InitDataRegis30(fromDate, toDate, UserLogon.UserName);

            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            return Ok(await _CDRServices.PrefixRegis30(model));
        }

        /// <summary>
        /// PrefixRegis30
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/CDR/PrefixRenew1Day")]
        [HttpPost]
        public async Task<IActionResult> PrefixRenew1Day([FromBody] PagingAllHisRegisRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            #endregion


            await _FTPServices.InitDataRenew1Day(fromDate, toDate, UserLogon.UserName);
            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            return Ok(await _CDRServices.PrefixRenew1Day(model));
        }

        /// <summary>
        /// CDR regis
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/CDR/CDRRegis")]
        [HttpPost]
        public async Task<IActionResult> CDRRegis([FromBody] PagingCDRRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.MPFromDate) || string.IsNullOrEmpty(model.MPToDate))
                return BadRequest("Vui lòng nhập đầy đủ thời gian");
            if (string.IsNullOrEmpty(model.Type)) return BadRequest("Sai rồi");


            var chkFrom = DateTime.TryParseExact(model.MPFromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.MPToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian MP");

            #endregion

            await _FTPServices.InitDataRegis1Day(fromDate.StartOfDay(), toDate.EndOfDay(), UserLogon.UserName);


            model.MPFromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.MPToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            return Ok(await _CDRServices.CDRRegis(model));
        }
        [Route("/CDR/CDRRegisExport")]
        [HttpPost]
        public async Task<IActionResult> CDRRegisExport([FromBody] PagingCDRRequestModel model)
        {
            #region VALIDATE
            if (string.IsNullOrEmpty(model.MPFromDate) || string.IsNullOrEmpty(model.MPToDate))
                return BadRequest("Vui lòng nhập đầy đủ thời gian");
            if (string.IsNullOrEmpty(model.Type)) return BadRequest("Sai rồi");


            var chkFrom = DateTime.TryParseExact(model.MPFromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.MPToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian MP");

            #endregion

            await _FTPServices.InitDataRegis1Day(fromDate.StartOfDay(), toDate.EndOfDay(), UserLogon.UserName);
            #region HANDLER
            model.MPFromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.MPToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            try
            {
                var contentRoot = _hostingEnvironment.ContentRootPath;
                string fileName = $"CDRRegisExport - {DateTime.Now:yyyyHHmmddMMhhss} - {UserLogon.UserName}.xlsx";
                string path = $"\\Export\\File\\{fileName}";
                string outputFile = contentRoot + path;
                var template = new XLTemplate($"{contentRoot}\\Export\\Template\\Report\\CDRRegisExport.xlsx");

                CDRRegisExportModel Result = new CDRRegisExportModel()
                {
                    FromDate = model.MPFromDate,
                    ToDate = model.MPToDate,
                    Details = await _CDRServices.CDRRegisExport(model)
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
        [Route("/CDR/PrefixRegis30Export")]
        [HttpPost]
        public async Task<IActionResult> PrefixRegis30Export([FromBody] PagingAllHisRegisRequestModel model)
        {

            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            #endregion
            await _FTPServices.InitDataRegis30(fromDate, toDate, UserLogon.UserName);
            #region HANDLER
            try
            {
                var contentRoot = _hostingEnvironment.ContentRootPath;
                string fileName = $"PrefixRegis30Export - {DateTime.Now:yyyyHHmmddMMhhss} - {UserLogon.UserName}.xlsx";
                string path = $"\\Export\\File\\{fileName}";
                string outputFile = contentRoot + path;
                var template = new XLTemplate($"{contentRoot}\\Export\\Template\\Report\\PrefixRegis30Export.xlsx");
                PrefixRegis30ExportModel Result = new PrefixRegis30ExportModel()
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Details = await _CDRServices.PrefixRegis30Export(model)
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
        [Route("/CDR/PrefixRegis1DayExport")]
        [HttpPost]
        public async Task<IActionResult> PrefixRegis1DayExport([FromBody] PagingAllHisRegisRequestModel model)
        {

            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            #endregion
            await _FTPServices.InitDataRegis1Day(fromDate, toDate, UserLogon.UserName);

            #region HANDLER
            try
            {
                var contentRoot = _hostingEnvironment.ContentRootPath;
                string fileName = $"PrefixRegis1DayExport - {DateTime.Now:yyyyHHmmddMMhhss} - {UserLogon.UserName}.xlsx";
                string path = $"\\Export\\File\\{fileName}";
                string outputFile = contentRoot + path;
                var template = new XLTemplate($"{contentRoot}\\Export\\Template\\Report\\PrefixRegis1DayExport.xlsx");
                PrefixRegis1DayExportModel Result = new PrefixRegis1DayExportModel()
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Details = await _CDRServices.PrefixRegis1DayExport(model)
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
        [Route("/CDR/PrefixRenew1DayExport")]
        [HttpPost]
        public async Task<IActionResult> PrefixRenew1DayExport([FromBody] PagingAllHisRegisRequestModel model)
        {

            #region VALIDATE
            if (string.IsNullOrEmpty(model.FromDate) || string.IsNullOrEmpty(model.ToDate))
                return BadRequest("Vui lòng nhập thời gian tìm kiếm");

            var chkFrom = DateTime.TryParseExact(model.FromDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
            var chkTo = DateTime.TryParseExact(model.ToDate, MPFormat.DateTime_103, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
            if (!chkFrom || !chkTo) return BadRequest("Sai định dạng thời gian tìm kiếm");

            model.FromDate = fromDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_FirstTime);
            model.ToDate = toDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm_LastTime);
            #endregion

            await _FTPServices.InitDataRenew1Day(fromDate, toDate, UserLogon.UserName);

            #region HANDLER
            try
            {
                var contentRoot = _hostingEnvironment.ContentRootPath;
                string fileName = $"PrefixRenew1DayExport - {DateTime.Now:yyyyHHmmddMMhhss} - {UserLogon.UserName}.xlsx";
                string path = $"\\Export\\File\\{fileName}";
                string outputFile = contentRoot + path;
                var template = new XLTemplate($"{contentRoot}\\Export\\Template\\Report\\PrefixRenew1DayExport.xlsx");
                PrefixRenew1DayExportModel Result = new PrefixRenew1DayExportModel()
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Details = await _CDRServices.PrefixRenew1DayExport(model)
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
        /// Lấy lịch chạy
        /// </summary>
        /// <returns></returns>
        [Route("/CDR/GetSchedule")]
        [HttpGet]
        public IActionResult GetSchedule()
        {
            try
            {
                CrontabSchedule _ScheduleRegis30 = CrontabSchedule.Parse(_FTPConfigModel.PrefixRegis30Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
                CrontabSchedule _ScheduleRegis1Day = CrontabSchedule.Parse(_FTPConfigModel.PrefixRegis1DaySchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
                CrontabSchedule _ScheduleRenew1Day = CrontabSchedule.Parse(_FTPConfigModel.PrefixRenew1DaySchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = false });
                DateTime _nextRegis30 = _ScheduleRegis30.GetNextOccurrence(DateTime.Now);
                DateTime _nextRegis1Day = _ScheduleRegis30.GetNextOccurrence(DateTime.Now);
                DateTime _nextRenew1Day = _ScheduleRegis30.GetNextOccurrence(DateTime.Now);

                return Ok(new
                {
                    Regis30 = _nextRegis30.ToString(MPFormat.DateTime_103Full),
                    Regis1Day = _nextRegis1Day.ToString(MPFormat.DateTime_103Full),
                    Renew1Day = _nextRenew1Day.ToString(MPFormat.DateTime_103Full)
                });
            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);
            }

            return Ok(new
            {
                Regis30 = "",
                Regis1Day = "",
                Renew1Day = ""
            });
        }

    }
}