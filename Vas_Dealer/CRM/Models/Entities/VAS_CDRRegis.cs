using MP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.VAS;

namespace VAS.Dealer.Models.Entities
{
    public class SP_CDRRegis
    {
        [Key]
        public Guid ID { get; set; }
        public int STT { get; set; }
        public string? MP_Phone { get; set; }
        public string? MP_TradeKey { get; set; }
        public string? VendorKey { get; set; }
        public string?  MP_Vendor { get; set; }
        public string? MP_Service { get; set; }
        public DateTime? MP_CreatedDate { get; set; }
        public string? MP_CreatedBy { get; set; }
        public string? VN_Trans_Id { get; set; }
        public string? VN_Branch_Code { get; set; }
        public string? VN_Service_Code { get; set; }
        public string? VN_Subscriber { get; set; }
        public string? VN_Price { get; set; }
        public string? VN_Charged_Price { get; set; }
        public DateTime? VN_RegisDate { get; set; }
        public int TotalRows { get; set; }
        public string MP_CreatedDateStr { get => MP_CreatedDate.HasValue ? MP_CreatedDate.Value.ToString(MPFormat.DateTime_103Full) : ""; }
        public string VN_RegisDateStr { get => VN_RegisDate.HasValue ? VN_RegisDate.Value.ToString(MPFormat.DateTime_103Full) : ""; }
    
    }

    public class SP_CDRRenew
    {
        public int STT { get; set; }
        public Guid ID { get; set; }
        public string MP_Phone { get; set; }
        public string MP_TradeKey { get; set; }
        public string MP_Vendor { get; set; }
        public string MP_Service { get; set; }
        public DateTime MP_CreatedDate { get; set; }
        public string MP_CreatedDateStr { get => MP_CreatedDate.ToString(MPFormat.DateTime_103Full); }
        public string MP_CreatedBy { get; set; }


        public string VN_Trans_Id { get; set; }
        public string VN_Branch_Code { get; set; }
        public string VN_Service_Code { get; set; }
        public string VN_Subscriber { get; set; }
        public string VN_Price { get; set; }
        public string VN_Charged_Price { get; set; }
        public string VN_RegisDate { get; set; }

        public int TotalRows { get; set; }
    }

    public class CDRRegisExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<SP_CDRRegis> Details { get; set; }
    }
    public class PrefixRegis30ExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<SP_CDR_PrefixRegis30> Details { get; set; }
    }
    public class PrefixRegis1DayExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<SP_CDR_PrefixRegis1Day> Details { get; set; }
    }
    public class PrefixRenew1DayExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<SP_CDR_PrefixRenew1Day> Details { get; set; }
    }
}
