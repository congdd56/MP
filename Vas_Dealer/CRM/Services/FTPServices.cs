using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MP.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;
using WinSCP;

namespace VAS.Dealer.Services
{
    /// <summary>
    /// - Mời gói
    /// TDL_MP_service_regis_hist_20210514125342.txt     =>>>>>>>>>>>>>>>>> Trả về với mỗi  30 phút
    /// - Gia hạn   
    /// TDL_MP_renew_20210514120030.txt                 =>>>>>>>>>>>>>>>>>>> Trả về vào cuối ngày
    /// - Tổng của mời gói
    /// TDL_MP_regis_20210514120035.txt                 =>>>>>>>>>>>>>>>>>> Trả về cuối ngày
    /// ----------------------------------------------------------------------------------
    /// Chỉ có thể đối soát được Mời gói 30 phút 1 ngày -> Đến cuối ngày sẽ đối soát lại với file tổng
    /// Gia hạn chỉ có thể đối soát vào cuối ngày
    /// </summary>
    public class FTPServices : IFTPServices
    {
        private readonly ILogger<FTPServices> _logger;
        private readonly MP_Context _Context;
        private readonly FTPConfigModel _FTPConfigModel;
        private SessionOptions _Ftp;
        private readonly IEmailServices _EmailServices;

        public FTPServices(ILogger<FTPServices> logger, MP_Context Context, IOptions<FTPConfigModel> FTPConfig,
            IEmailServices EmailServices)
        {
            _logger = logger;
            _EmailServices = EmailServices;
            _Context = Context;
            _FTPConfigModel = FTPConfig.Value;
            InitSession();
        }

        /// <summary>
        /// Khởi tạo session ftp
        /// </summary>
        void InitSession()
        {
            // Setup session options
            _Ftp = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = _FTPConfigModel.IP,
                UserName = _FTPConfigModel.UserName,
                Password = _FTPConfigModel.Password,
                PortNumber = _FTPConfigModel.Port,
                FtpMode = _FTPConfigModel.PassiveMode ? FtpMode.Passive : FtpMode.Active
            };
        }

        /// <summary>
        /// Lấy danh sách file name trong folder
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileName()
        {
            try
            {
                using (Session session = new Session())
                {
                    session.Open(_Ftp);

                    return session.ListDirectory(session.HomePath).Files.Where(x => !x.IsDirectory).Select(s => s.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _EmailServices.SendWarningFtpError(ex.Message);
            }
            return null;
        }


        /// <summary>
        /// Lấy danh sách file theo ngày
        /// Định dạng file theo config PrefixRegis30
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<string> GetListFileRegis30NameByDay(DateTime date)
        {
            List<string> result = new List<string>();
            List<string> allFiles = GetAllFileName();
            if (allFiles != null && allFiles.Count > 0)
                result = allFiles.Where(x => x.Contains($"{_FTPConfigModel.PrefixRegis30}{date.ToString(MPFormat.yyyyMMdd)}")).ToList();
            return result;
        }


        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRegis1Day
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task InitDataRegis1Day(DateTime fromDate, DateTime toDate, string userName)
        {
            #region Khởi tạo tên file cần tải

            List<string> result = new List<string>();
            List<string> allFiles = GetAllFileName();

            int number = (int)(toDate - fromDate).TotalDays;
            var files = new List<string>();
            for (int i = 0; i <= number; i++)
            {
                var strCheck = $"{_FTPConfigModel.PrefixRegis1Day}{fromDate.AddDays(i).ToString(MPFormat.yyyyMMdd)}";

                if (allFiles != null && allFiles.Count > 0)
                    files = allFiles.Where(x => x.Contains(strCheck)).ToList();
                result.AddRange(files);
            }

            #endregion

            #region Loại bỏ tất cả file đã tải

            var allDled = FileDownloaded(fromDate, toDate);

            result = result.Where(x => !allDled.Contains(x)).ToList();
            if (result.Count > 0)
            {
                await GetFiles(result, userName);
            }

            #endregion
        }


        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRegis30
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task InitDataRegis30(DateTime fromDate, DateTime toDate, string userName)
        {
            #region Khởi tạo tên file cần tải

            List<string> result = new List<string>();
            List<string> allFiles = GetAllFileName();

            int number = (int)(toDate - fromDate).TotalDays;
            var files = new List<string>();
            for (int i = 0; i <= number; i++)
            {
                if (allFiles != null && allFiles.Count > 0)
                    files = allFiles.Where(x => x.Contains($"{_FTPConfigModel.PrefixRegis30}{fromDate.AddDays(i).ToString(MPFormat.yyyyMMdd)}")).ToList();
                result.AddRange(files);
            }

            #endregion


            #region Loại bỏ tất cả file đã tải

            var allDled = FileDownloaded(fromDate, toDate);

            result = result.Where(x => !allDled.Contains(x)).ToList();
            if (result.Count > 0)
            {
                await GetFiles(result, userName);
            }

            #endregion
        }


        /// <summary>
        /// Khởi tạo dữ liệu cho báo cáo
        /// Định dạng file theo config PrefixRenew1Day
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task InitDataRenew1Day(DateTime fromDate, DateTime toDate, string userName)
        {
            #region Khởi tạo tên file cần tải

            List<string> result = new List<string>();
            List<string> allFiles = GetAllFileName();

            int number = (int)(toDate - fromDate).TotalDays;
            var files = new List<string>();
            for (int i = 0; i <= number; i++)
            {
                if (allFiles != null && allFiles.Count > 0)
                    files = allFiles.Where(x => x.Contains($"{_FTPConfigModel.PrefixRenew1Day}{fromDate.AddDays(i).ToString(MPFormat.yyyyMMdd)}")).ToList();
                result.AddRange(files);
            }

            #endregion


            #region Loại bỏ tất cả file đã tải

            var allDled = FileDownloaded(fromDate, toDate);

            result = result.Where(x => !allDled.Contains(x)).ToList();
            if (result.Count > 0)
            {
                await GetFiles(result, userName);
            }

            #endregion
        }



        /// <summary>
        /// Lấy danh sách file theo ngày
        /// Định dạng file theo config PrefixRenew1Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<string> GetListFileRenew1DayNameByDay(DateTime date)
        {
            List<string> result = new List<string>();
            List<string> allFiles = GetAllFileName();
            if (allFiles != null && allFiles.Count > 0)
                result = allFiles.Where(x => x.Contains($"{_FTPConfigModel.PrefixRenew1Day}{date.ToString(MPFormat.yyyyMMdd)}")).ToList();
            return result;
        }

        /// <summary>
        /// Tải file về
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> GetFiles(List<string> fileNames, string userName)
        {
            if (fileNames == null || fileNames.Count == 0) return false;

            try
            {
                using (Session session = new Session())
                {
                    session.Open(_Ftp);

                    // Download files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;


                    var check = false;
                    TransferOperationResult transferResult;
                    var files = session.ListDirectory(session.HomePath).Files.Where(x => !x.IsDirectory).ToList();
                    foreach (var file in fileNames)
                    {
                        transferResult = session.GetFiles($"{session.HomePath}/{file}",
                            $"{_FTPConfigModel.DownloadPath}", false, transferOptions);

                        // Throw on any error
                        transferResult.Check();

                        // Print results
                        foreach (TransferEventArgs transfer in transferResult.Transfers)
                        {
                            check = true;

                            //Chuyển dữ liệu file download được vào db
                            var checkConvert = await ConvertData(file, userName);

                            //Lưu lịch sử
                            await SaveFtpHistory(new VAS_FtpHistory()
                            {
                                FileName = file,
                                FileDate = GetFileDate(file, out string type),
                                CreatedBy = userName,
                                FileType = type,
                                CreatedDate = DateTime.Now,
                                ConvertToData = checkConvert
                            });
                        }
                    }
                    return check;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _EmailServices.SendWarningFtpError(ex.Message);

                return false;
            }
        }


        /// <summary>
        /// Kiểm tra trong khoảng thời gian đã tải file chưa
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<string> FileDownloaded(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.StartOfDay();
            toDate = toDate.EndOfDay();
            return _Context.FtpHistory.Where(x => x.FileDate.HasValue && x.FileDate.Value >= fromDate
                        && x.FileDate.Value <= toDate && x.ConvertToData)
                .Select(s => s.FileName).ToList();
        }

        /// <summary>
        /// Lưu lịch sử tải file
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        async Task SaveFtpHistory(VAS_FtpHistory model)
        {
            model.Id = Guid.NewGuid();
            await _Context.FtpHistory.AddAsync(model);
            await _Context.SaveChangesAsync();
        }

        /// <summary>
        /// Tách tên file ra lấy thời gian
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        DateTime? GetFileDate(string fileName, out string fileType)
        {
            fileType = string.Empty;

            if (fileName.Contains(_FTPConfigModel.PrefixRegis1Day))
            {
                string date = fileName.Replace(_FTPConfigModel.PrefixRegis1Day, "");
                if (date.Contains("."))
                {
                    date = date.Split('.')[0];
                    fileType = _FTPConfigModel.PrefixRegis1Day;
                }

                var checkDate = DateTime.TryParseExact(date, MPFormat.yyyyMMddHHmmss,
                       CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                if (checkDate) return dt;
                else return null;
            }


            if (fileName.Contains(_FTPConfigModel.PrefixRegis30))
            {
                string date = fileName.Replace(_FTPConfigModel.PrefixRegis30, "");
                if (date.Contains("."))
                {
                    date = date.Split('.')[0];
                    fileType = _FTPConfigModel.PrefixRegis30;
                }

                var checkDate = DateTime.TryParseExact(date, MPFormat.yyyyMMddHHmmss,
                       CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                if (checkDate) return dt;
                else return null;
            }


            if (fileName.Contains(_FTPConfigModel.PrefixRenew1Day))
            {
                string date = fileName.Replace(_FTPConfigModel.PrefixRenew1Day, "");
                if (date.Contains("."))
                {
                    date = date.Split('.')[0];
                    fileType = _FTPConfigModel.PrefixRenew1Day;
                }

                var checkDate = DateTime.TryParseExact(date, MPFormat.yyyyMMddHHmmss,
                       CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                if (checkDate) return dt;
                else return null;
            }

            return null;
        }

        public async Task<bool> ConvertData(string fileName, string userName)
        {
            try
            {
                #region PrefixRegis1Day
                if (fileName.Contains(_FTPConfigModel.PrefixRegis1Day))
                {
                    string line;
                    List<CDR_PrefixRegis1Day> data = new List<CDR_PrefixRegis1Day>();

                    // Read the file and display it line by line.
                    System.IO.StreamReader file = new System.IO.StreamReader($"{_FTPConfigModel.DownloadPath}/{fileName}");
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');

                        var _RegisDate = DateTime.TryParseExact(words[6], MPFormat.Exten_ddMMyyyyHHmmss, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime regisDate);

                        var itemData = new CDR_PrefixRegis1Day()
                        {
                            Id = Guid.NewGuid(),
                            Trans_Id = words[0],
                            Branch_Code = words[1],
                            Service_Code = words[2],
                            Subscriber = words[3],
                            Price = words[4],
                            Charged_price = words[5],
                            Regis_Date = words[6],
                            Status = words[7],
                            Subs_Type = words[8],
                            Active_Date = words[9],
                            FileName = fileName,
                            FileDate = GetFileDate(fileName, out string type),
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now
                        };
                        if (_RegisDate) itemData.RegisDate = regisDate;
                        data.Add(itemData);
                    }
                    file.Close();

                    #region Xóa bản ghi đầu tiên là header

                    if (data.Count > 0) data.RemoveAll(x => x.Trans_Id == "trans_id");

                    #endregion

                    #region Lưu dữ liệu

                    if (data.Count > 0)
                    {
                        await _Context.VAS_PrefixRegis1Day.AddRangeAsync(data);
                        await _Context.SaveChangesAsync();
                    }
                    #endregion


                    return true;
                }
                #endregion

                #region PrefixRegis30
                if (fileName.Contains(_FTPConfigModel.PrefixRegis30))
                {
                    string line;
                    List<CDR_PrefixRegis30> data = new List<CDR_PrefixRegis30>();

                    // Read the file and display it line by line.
                    System.IO.StreamReader file = new System.IO.StreamReader($"{_FTPConfigModel.DownloadPath}/{fileName}");
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');
                        var _RegisDate = DateTime.TryParseExact(words[6], MPFormat.Exten_ddMMyyyyHHmmss, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime regisDate);


                        var itemData = new CDR_PrefixRegis30()
                        {
                            Id = Guid.NewGuid(),
                            Trans_Id = words[0],
                            Branch_Code = words[1],
                            Service_Code = words[2],
                            Subscriber = words[3],
                            Price = words[4],
                            Charged_Price = words[5],
                            FileDate = GetFileDate(fileName, out string type),
                            Regis_Date = words[6],
                            Status = words[7],
                            Error_Mess = words[8],
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            FileName = fileName
                        };
                        data.Add(itemData);
                        if (_RegisDate) itemData.RegisDate = regisDate;
                        data.Add(itemData);

                    }
                    file.Close();

                    #region Xóa bản ghi đầu tiên là header

                    if (data.Count > 0) data.RemoveAll(x => x.Trans_Id == "trans_id");

                    #endregion

                    #region Lưu dữ liệu

                    if (data.Count > 0)
                    {
                        await _Context.VAS_PrefixRegis30.AddRangeAsync(data);
                        await _Context.SaveChangesAsync();
                    }
                    #endregion


                    return true;
                }
                #endregion

                #region PrefixRenew1Day
                if (fileName.Contains(_FTPConfigModel.PrefixRenew1Day))
                {
                    string line;
                    List<CDR_PrefixRenew1Day> data = new List<CDR_PrefixRenew1Day>();

                    // Read the file and display it line by line.
                    System.IO.StreamReader file = new System.IO.StreamReader($"{_FTPConfigModel.DownloadPath}/{fileName}");
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');
                        var _RegisDate = DateTime.TryParseExact(words[5], MPFormat.Exten_ddMMyyyyHHmmss, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime regisDate);
                        var _RenewDate = DateTime.TryParseExact(words[4], MPFormat.Exten_ddMMyyyyHHmmss, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime renewDate);

                        var itemData = new CDR_PrefixRenew1Day()
                        {
                            Id = Guid.NewGuid(),
                            Trans_Id = words[0],
                            Branch_Code = words[1],
                            Service_Code = words[2],
                            Charged_Price = words[3],
                            Renew_Date = words[4],
                            Regis_Date = words[5],
                            Subs_Type = words[6],
                            Active_Date = words[7],
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            FileDate = GetFileDate(fileName, out string type),

                        };
                        data.Add(itemData);
                        if (_RegisDate) itemData.RegisDate = regisDate;
                        if (_RenewDate) itemData.RenewDate = renewDate;
                        data.Add(itemData);


                    }
                    file.Close();


                    #region Xóa bản ghi đầu tiên là header

                    if (data.Count > 0) data.RemoveAll(x => x.Trans_Id == "trans_id");

                    #endregion

                    #region Lưu dữ liệu

                    if (data.Count > 0)
                    {
                        await _Context.VAS_PrefixRenew1Day.AddRangeAsync(data);
                        await _Context.SaveChangesAsync();
                    }
                    #endregion

                    return true;
                }
                #endregion

                _EmailServices.SendWarningFtpError("Định dạng file trên ftp không đúng. Đã bị thay đổi");
                return false;
            }
            catch (Exception ex)
            {
                _EmailServices.SendWarningFtpError(ex.Message);
                _logger.LogError(ex.Message, ex);

                return false;
            }
        }
    }
}
