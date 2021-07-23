using Microsoft.EntityFrameworkCore;
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
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class CDRServices : ICDRServices
    {
        private readonly ILogger<CDRServices> _logger;
        private readonly MP_Context _Context;
        private readonly FTPConfigModel _FTPConfigModel;
        private readonly IEmailServices _EmailServices;
        private readonly IFTPServices _FTPServices;

        public CDRServices(ILogger<CDRServices> logger, MP_Context Context, IOptions<FTPConfigModel> FTPConfig,
            IEmailServices EmailServices, IFTPServices FTPServices)
        {
            _logger = logger;
            _EmailServices = EmailServices;
            _Context = Context;
            _FTPConfigModel = FTPConfig.Value;
            _FTPServices = FTPServices;
        }





        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagingResultModel> PrefixRegis1Day(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRegis1Day }] '{page.FromDate}', '{page.ToDate}',{page.start}, {page.length}, N'{page.search.value}','0',N'{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRegis1Day.FromSqlRaw(sql).ToListAsync();
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
        public async Task<PagingResultModel> PrefixRegis30(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRegis30 }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','0',N'{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRegis30.FromSqlRaw(sql).ToListAsync();
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
        public async Task<PagingResultModel> PrefixRenew1Day(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRenew1Day }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','0','{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRenew1Day.FromSqlRaw(sql).ToListAsync();
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
        /// Đối soát mời gói
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PagingResultModel> CDRRegis(PagingCDRRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;

            try
            {
                using (var command = _Context.Database.GetDbConnection().CreateCommand())
                {
                    var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_Regis }] '{page.MPFromDate}', '{page.MPToDate}', '{page.Type}', {page.start}, {page.length}, N'{page.search.value}','0','{page.User}','{page.Branch}'";
                    var result = await _Context.SP_CDRRegis.FromSqlRaw(sql).ToListAsync();
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
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<SP_CDRRegis>> CDRRegisExport(PagingCDRRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;

            try
            {
                using (var command = _Context.Database.GetDbConnection().CreateCommand())
                {
                    var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_Regis }] '{page.MPFromDate}', '{page.MPToDate}', '{page.Type}', {page.start}, {page.length}, N'{page.search.value}',N'1','{page.User}','{page.Branch}'";
                    var result =  await _Context.SP_CDRRegis.FromSqlRaw(sql).ToListAsync();
                    var counter = page.start;
                    result.ForEach(x =>
                    {
                        counter++;
                        x.STT = counter;
                    });
                    return result;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<SP_CDR_PrefixRegis30>> PrefixRegis30Export(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRegis30 }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','1','{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRegis30.FromSqlRaw(sql).ToListAsync();
                var counter = page.start;
                result.ForEach(x =>
                {
                    counter++;
                    x.STT = counter;
                });

                return result;
            }
        }
        public async Task<List<SP_CDR_PrefixRegis1Day>> PrefixRegis1DayExport(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRegis1Day }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','1','{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRegis1Day.FromSqlRaw(sql).ToListAsync();
                var counter = page.start;
                result.ForEach(x =>
                {
                    counter++;
                    x.STT = counter;
                });
                return result;
            }
        }
        public async Task<List<SP_CDR_PrefixRenew1Day>> PrefixRenew1DayExport(PagingAllHisRegisRequestModel page)
        {
            page.search = page.search == null ? page.search = new Search() : page.search;
            page.length = 10;
            using (var command = _Context.Database.GetDbConnection().CreateCommand())
            {
                var sql = $"EXEC [dbo].[{MPStoreConst.SP_CDR_PrefixRenew1Day }] '{page.FromDate}', '{page.ToDate}', {page.start}, {page.length}, N'{page.search.value}','1','{page.User}','{page.Branch}'";
                var result = await _Context.SP_CDR_PrefixRenew1Day.FromSqlRaw(sql).ToListAsync();
                var counter = page.start;
                result.ForEach(x =>
                {
                    counter++;
                    x.STT = counter;
                });

                return result;
            }
        }
    }
}
