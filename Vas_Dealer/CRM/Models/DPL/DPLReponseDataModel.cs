using MP.Common;
using System;

namespace VAS.Dealer.Models.DPL
{
    public class LoginReponseModel
    {
        public bool authenticated { get; set; }
        public string timestamp { get; set; }
        public string token { get; set; }
        public string userName { get; set; }
    }

    public class MasterDataProductModel
    {
        public string ItemGroupName { get; set; }
        public string ItemGroupId { get; set; }
        public string ItemId { get; set; }
        public string NameAlias { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string TrackingDimension { get; set; }
        public string UnitId { get; set; }
        public string status { get; set; }
    }

    public class MasterDataProductVariantModel
    {
        public string DisplayProductNumber { get; set; }
        public string InventColorId { get; set; }
        public string InventSizeId { get; set; }
        public string InventStyleId { get; set; }
        public string ItemId { get; set; }
        public string RetailVariantId { get; set; }
        public string Status { get; set; }
        public string configId { get; set; }
    }

    public class MPLoadASCModel
    {
        public string AccountNum { get; set; }
        public string Address { get; set; }
        public string CustGroup { get; set; }
        public string Employeeresponsible { get; set; }
        public string MainContactWorker { get; set; }
        public string Name { get; set; }
        public string SalesDistrictId { get; set; }
        public string SegmentId { get; set; }
        public string Status { get; set; }
        public string SubsegmentId { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
    }
    public class MPLoadContactPersonASCModel
    {
        public string AccountNum { get; set; }
        public string ContactPersonId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class MPLoadEmployeeModel
    {
        public string Department { get; set; }
        public string DepartmentName { get; set; }
        public string FullName { get; set; }
        public string HcmWorker { get; set; }
        public string OMOperatingUnitNumberChild { get; set; }
        public string PersonnelNumber { get; set; }
        public string Status { get; set; }
    }


    public class ConditionsWarrantyModel
    {
        public Guid Id { get; set; }
        public string DATEEND { get; set; }
        public string DATESTART { get; set; }
        public string GUARANTEE { get; set; }
        public string INVENTITEMGROUP { get; set; }
        public string NAME { get; set; }
        public string NAMEITEMGROUP { get; set; }
        public string Status { get; set; }
    }

    public class MPLoadCheckProductStatusModel
    {
        public string Color { get; set; }
        public string Description { get; set; }
        public string InventSizeId { get; set; }
        public string InventStyleId { get; set; }
        public string ItemGroupId { get; set; }
        public string ItemID { get; set; }
        public string Month { get; set; }
        public string SerialNum { get; set; }
        public string Status { get; set; }
        public string configId { get; set; }
    }

    public class MPLoadEISerrialModel
    {
        public string Address { get; set; }
        public string CategoryID { get; set; }
        public string DATEOFPURCHASE { get; set; }
        public string Email { get; set; }
        public string ExpDate { get; set; }
        public string FullName { get; set; }
        public string INVENTSERIALID { get; set; }
        public string ItemID { get; set; }
        public string Model { get; set; }
        public string PackingDate { get; set; }
        public string Phone { get; set; }
        public string SalesDistrictId { get; set; }
        public string Status { get; set; }
        public string SubSegmentId { get; set; }
    }

    #region DPL_MPLoadEWarrantyView

    public class MPLoadEWarrantyViewModel
    {
        public string CUSTACCOUNT { get; set; }
        public string DELIVERYNAME { get; set; }
        public string INVENTCOLORID { get; set; }
        public string INVENTLOCATIONID { get; set; }
        public string INVENTSERIALID { get; set; }
        public string INVENTSITEID { get; set; }
        public string INVENTSIZEID { get; set; }
        public string INVENTSTYLEID { get; set; }
        public string INVOICEDATE { get; set; }
        public string ITEMGROUPID { get; set; }
        public string ITEMID { get; set; }
        public string NAMEALIAS { get; set; }
        public string PACKINGSLIPDATE { get; set; }
        public string PACKINGSLIPID { get; set; }
        public string SALESID { get; set; }
        public string WMSLOCATIONID { get; set; }
        public string status { get; set; }

    }

    public class MPLoadEWarrantyViewRequestModel
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
    }


    #endregion



    public class MPLoadDNCLKPTModel
    {
        public string CaseID { get; set; }
        public string DPL_DeliveryStatus { get; set; }
        public string DPL_TTBHPhieuKTTTKT { get; set; }
        public string DeliveryNumber { get; set; }
        public string FileName { get; set; }
        public string NumSeqPDNCLKPT { get; set; }
        public string PackingSlipId { get; set; }
        public string SalesId { get; set; }
        public string Status { get; set; }
        public DateTime? TransDateReceivedPTLK { get; set; }
        public string TransDateReceivedPTLKStr { get => TransDateReceivedPTLK.HasValue ? TransDateReceivedPTLK.Value.ToString(MPFormat.DateTime_103) : string.Empty; }
        public DateTime? TransDateRecivedTBH { get; set; }
        public string TransDateRecivedTBHStr { get => TransDateRecivedTBH.HasValue ? TransDateRecivedTBH.Value.ToString(MPFormat.DateTime_103) : string.Empty; }
        public string Transport { get; set; }
    }

    public class GetMPLoadPKTTTKTModel
    {
        public string DataId { get; set; }
        public string CaseID { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// Mã phiếu KT TTKT
        /// </summary>
        public string NumSeqPKTTTKT { get; set; }
        public string PKTStatus { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Status { get; set; }
        public DateTime? TransDateClose { get; set; }
        public string TransDateCloseStr { get => TransDateClose.HasValue ? TransDateClose.Value.ToString(MPFormat.DateTime_103) : string.Empty; }
        /// <summary>
        /// Ngày tạo phiếu KT TTKT
        /// </summary>
        public DateTime? TransDateCreate { get; set; }
        /// <summary>
        /// Ngày tạo phiếu KT TTKT
        /// </summary>
        public string TransDateCreateStr { get => TransDateCreate.HasValue ? TransDateCreate.Value.ToString(MPFormat.DateTime_103) : string.Empty; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_103); }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateStr { get => UpdatedDate.HasValue ? UpdatedDate.Value.ToString(MPFormat.DateTime_103) : string.Empty; }

    }

    public class MPLoadSCLKPTModel
    {
        public string CaseID { get; set; }
        public string DPL_TTBHPhieuKTTTKT { get; set; }
        public string DPL_TTBHPhieuSC { get; set; }
        public string FileName { get; set; }
        public string SCLKPTStatus { get; set; }
        public string Status { get; set; }
    }

    public class UpPhieuKTTTKTModel
    {
        public string CaseID { get; set; }
        public string CategoryID { get; set; }
        public string Color { get; set; }
        public string Config { get; set; }
        public string DPLProofOfPurchaseType { get; set; }
        public string ErrorCode { get; set; }
        public string InventSerialId { get; set; }
        public string ItemID { get; set; }
        public string MPStatus { get; set; }
        public string MerchandiseStatus { get; set; }
        public string Model { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string NumSeqRetailCMStore { get; set; }
        public string ProductStatus { get; set; }
        public string PurchaseDate { get; set; }
        public string Solution { get; set; }
        public string Status { get; set; }
        public string TechnicalErrorStaust { get; set; }
        public string TransDateCreate { get; set; }
        public string VoucherPurchaseNumb { get; set; }
        public string Voucherpurchase { get; set; }
    }

    public class UPRetailCMModel
    {
        public string ContactName { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string PHONE { get; set; }
        public string RetailCMName { get; set; }
        public string RetailCMTyple { get; set; }
        public string SALESDISTRICTID { get; set; }
        public string SEGMENTID { get; set; }
        public string STREET { get; set; }
        public string SUBSEGMENTID { get; set; }
        public string Status { get; set; }
        public string StatusNew { get; set; }
    }
}
