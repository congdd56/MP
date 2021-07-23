using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;

namespace VAS.Dealer.Services.Interfaces
{
    public interface ICDRServices
    {
        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagingResultModel> PrefixRegis1Day(PagingAllHisRegisRequestModel page);
        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagingResultModel> PrefixRegis30(PagingAllHisRegisRequestModel page);
        /// <summary>
        /// Giành cho quản trị tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagingResultModel> PrefixRenew1Day(PagingAllHisRegisRequestModel page);
        /// <summary>
        /// Đối soát mời gói
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PagingResultModel> CDRRegis(PagingCDRRequestModel page);
        Task<List<SP_CDRRegis>> CDRRegisExport(PagingCDRRequestModel page);
        Task<List<SP_CDR_PrefixRegis30>> PrefixRegis30Export(PagingAllHisRegisRequestModel page);
        Task<List<SP_CDR_PrefixRegis1Day>> PrefixRegis1DayExport(PagingAllHisRegisRequestModel page);
        Task<List<SP_CDR_PrefixRenew1Day>> PrefixRenew1DayExport(PagingAllHisRegisRequestModel page);
    }
}
