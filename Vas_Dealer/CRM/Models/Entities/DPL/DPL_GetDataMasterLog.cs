using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_GetDataMasterLog
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MasterDataProduct { get; set; }
        public int MasterDataProductVariant { get; set; }
        public int MPLoadASC { get; set; }
        public int MPLoadContactPersonASC { get; set; }
        public int MPLoadEmployee { get; set; }
        public int ConditionsWarranty { get; set; }
        public int MPLoadCheckProductStatus { get; set; }
        public int MPLoadEISerrial { get; set; }
        public int MPLoadEWarrantyView { get; set; }


        public bool CheckMasterDataProduct { get; set; }
        public bool CheckMasterDataProductVariant { get; set; }
        public bool CheckMPLoadASC { get; set; }
        public bool CheckMPLoadContactPersonASC { get; set; }
        public bool CheckMPLoadEmployee { get; set; }
        public bool CheckConditionsWarranty { get; set; }
        public bool CheckMPLoadCheckProductStatus { get; set; }
        public bool CheckMPLoadEISerrial { get; set; }
        public bool CheckMPLoadEWarrantyView { get; set; }


        public bool SaveMasterDataProduct { get; set; }
        public bool SaveMasterDataProductVariant { get; set; }
        public bool SaveMPLoadASC { get; set; }
        public bool SaveMPLoadContactPersonASC { get; set; }
        public bool SaveMPLoadEmployee { get; set; }
        public bool SaveConditionsWarranty { get; set; }
        public bool SaveMPLoadCheckProductStatus { get; set; }
        public bool SaveMPLoadEISerrial { get; set; }
        public bool SaveMPLoadEWarrantyView { get; set; }



        public DateTime BeginMasterDataProduct { get; set; }
        public DateTime EndMasterDataProduct { get; set; }
        public DateTime BeginMasterDataProductVariant { get; set; }
        public DateTime EndMasterDataProductVariant { get; set; }
        public DateTime BeginMPLoadASC { get; set; }
        public DateTime EndMPLoadASC { get; set; }
        public DateTime BeginMPLoadContactPersonASC { get; set; }
        public DateTime EndMPLoadContactPersonASC { get; set; }
        public DateTime BeginMPLoadEmployee { get; set; }
        public DateTime EndMPLoadEmployee { get; set; }
        public DateTime BeginConditionsWarranty { get; set; }
        public DateTime EndConditionsWarranty { get; set; }
        public DateTime BeginMPLoadCheckProductStatus { get; set; }
        public DateTime EndMPLoadCheckProductStatus { get; set; }
        public DateTime BeginMPLoadEISerrial { get; set; }
        public DateTime EndMPLoadEISerrial { get; set; }
        public DateTime BeginMPLoadEWarrantyView { get; set; }
        public DateTime EndMPLoadEWarrantyView { get; set; }
        public string ErrorMsg { get; set; }

    }
}
