using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_MPLoadEmployee
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public string DepartmentName { get; set; }
        public string FullName { get; set; }
        public string HcmWorker { get; set; }
        public string OMOperatingUnitNumberChild { get; set; }
        public string PersonnelNumber { get; set; }
        public string Status { get; set; }
    }
}
