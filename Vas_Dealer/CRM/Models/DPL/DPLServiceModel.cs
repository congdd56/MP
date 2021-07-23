namespace VAS.Dealer.Models.DPL
{
    public class DPLServiceURLModel
    {
        public string URLService { get; set; }
    }


    public class DPLServiceModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Uri { get; set; }
        public string Uri_DEV { get; set; }
        public DPLServiceDetailModel Login { get; set; }
        public DPLServiceDetailModel ConfirmConditionsWarranty { get; set; }
        public DPLServiceDetailModel ConfirmGetMasterDataProduct { get; set; }
        public DPLServiceDetailModel ConfirmGetMasterDataProductVariant { get; set; }
        public DPLServiceDetailModel ConfirmMPContactPersonASC { get; set; }
        public DPLServiceDetailModel ConfirmMPLoadASC { get; set; }
        public DPLServiceDetailModel ConfirmMPLoadCheckProductStatus { get; set; }
        public DPLServiceDetailModel ConfirmMPLoadEISerrial { get; set; }
        public DPLServiceDetailModel ConfirmMPLoadEmployee { get; set; }
        public DPLServiceDetailModel ConfirmMPLoadEWarrantyView { get; set; }
        public DPLServiceDetailModel GetConditionsWarranty { get; set; }
        public DPLServiceDetailModel GetMasterDataProduct { get; set; }
        public DPLServiceDetailModel GetMasterDataProductVariant { get; set; }
        public DPLServiceDetailModel GetMPLoadASC { get; set; }
        public DPLServiceDetailModel GetMPLoadCheckProductStatus { get; set; }
        public DPLServiceDetailModel GetMPLoadContactPersonASC { get; set; }
        public DPLServiceDetailModel GetMPLoadDNCLKPT { get; set; }
        public DPLServiceDetailModel GetMPLoadEISerrial { get; set; }
        public DPLServiceDetailModel GetMPLoadEmployee { get; set; }
        public DPLServiceDetailModel GetMPLoadEWarrantyView { get; set; }
        public DPLServiceDetailModel GetMPLoadPKTTTKT { get; set; }
        public DPLServiceDetailModel GetMPLoadSCLKPT { get; set; }
        public DPLServiceDetailModel GetUpPhieuKTTTKT { get; set; }
        public DPLServiceDetailModel GetUPRetailCM { get; set; }
        public DPLServiceDetailModel InsertMPUpPhieuKTTTKT { get; set; }
        public DPLServiceDetailModel InsertRetailCM { get; set; }
        public DPLServiceDetailModel IsValidateUser { get; set; }
    }

    public class DPLServiceDetailModel
    {
        public string Service { get; set; }
        public string Method { get; set; }
    }
}
