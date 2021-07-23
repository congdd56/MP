using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace VAS.Dealer.Models.Entities
{
    public class VAS_Registered
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Vendor { get; set; }
        public string Services { get; set; }
        public string Type { get; set; }
        public int TypeKey { get => Type == "Mời gói" ? 1 : 2; }
        public string TradeKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class VAS_Vendor
    {
        public int Id { get; set; }
        public string Branch_code { get; set; }
        public string Branch_name { get; set; }
        public string Branch_mobile { get; set; }
        public string Branch_sales_code { get; set; }
        public int? AccId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteReasion { get; set; }
        public virtual MP_Account Account { get; set; }
    }

    public class VAS_ServicesLog
    {
        public int Id { get; set; }
        public string LOAI_DVU { get; set; }
        public string DVU { get; set; }
        public string GIA { get; set; }
        public string TTHAI { get; set; }
        public string MT { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateLog { get; set; }
    }

    public class VAS_AuthenProperty
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string ErrorCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class VAS_FtpHistory
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime? FileDate { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool ConvertToData { get; set; }
        public string CreatedBy { get; set; }
    }



    public class CDR_PrefixRenew1Day
    {
        public Guid Id { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string Charged_Price { get; set; }
        public string Renew_Date { get; set; }
        public string Regis_Date { get; set; }
        public DateTime? RegisDate { get; set; }
        public DateTime? RenewDate { get; set; }
        public DateTime? FileDate { get; set; }
        public string Subs_Type { get; set; }
        public string Active_Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string FileName { get; set; }
    }

    public class SP_CDR_PrefixRenew1Day
    {
        [Key]
        public Guid Id { get; set; }
        public int STT { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Charged_Price { get; set; }
        public string Renew_Date { get; set; }
        public DateTime? RenewDate { get; set; }
        public string RenewDateStr { get => RenewDate.HasValue ? RenewDate.Value.ToString(MPFormat.DateTime_103Full) : Renew_Date; }
        public string Regis_Date { get; set; }
        public DateTime? RegisDate { get; set; }
        public string RegisDateStr { get => RegisDate.HasValue ? RegisDate.Value.ToString(MPFormat.DateTime_103Full) : Regis_Date; }
        public string Subs_Type { get; set; }
        public string Active_Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalRows { get; set; }
    }


    public class CDR_PrefixRegis30
    {
        public Guid Id { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string Subscriber { get; set; }
        public string Price { get; set; }
        public string Charged_Price { get; set; }
        public string Regis_Date { get; set; }
        public DateTime? RegisDate { get; set; }
        public DateTime? FileDate { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Error_Mess { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class SP_CDR_PrefixRegis30
    {
        [Key]
        public Guid Id { get; set; }
        public int STT { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Subscriber { get; set; }
        public string Price { get; set; }
        public string Charged_Price { get; set; }
        public string Regis_Date { get; set; }
        public virtual string Regis_DateStr
        {
            get
            {
                var chkFrom = DateTime.TryParseExact(Regis_Date, MPFormat.Exten_ddMMyyyyHHmmss, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
                if (chkFrom) return fromDate.ToString(MPFormat.DateTime_103Full);
                else return Regis_Date;
            }
        }
        public string Status { get; set; }
        public string Error_Mess { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int TotalRows { get; set; }
    }

    public class CDR_PrefixRegis1Day
    {
        public Guid Id { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string Subscriber { get; set; }
        public string Price { get; set; }
        public string Charged_price { get; set; }
        public string Regis_Date { get; set; }
        public DateTime? RegisDate { get; set; }
        public string Status { get; set; }
        public string Subs_Type { get; set; }
        public string Active_Date { get; set; }
        public DateTime? FileDate { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class SP_CDR_PrefixRegis1Day
    {
        [Key]
        public Guid Id { get; set; }
        public int STT { get; set; }
        public string Trans_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Service_Code { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Subscriber { get; set; }
        public string Charged_price { get; set; }
        public string Regis_Date { get; set; }
        public DateTime? RegisDate { get; set; }
        public virtual string RegisDateStr { get => RegisDate.HasValue ? RegisDate.Value.ToString(MPFormat.DateTime_103Full) : Regis_Date; }
        public string Status { get; set; }
        public string Subs_Type { get; set; }
        public string Active_Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int TotalRows { get; set; }
        public string Price { get; set; }
    }
}
