using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_ConditionsWarranty
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
}
