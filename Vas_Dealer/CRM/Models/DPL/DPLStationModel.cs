using System;

namespace VAS.Dealer.Models.DPL
{
    public class DPLStationModel
    {
        public int STT { get; set; }
        public Guid Id { get; set; }
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

    public class DPLStationSearchModel
    {
        public int draw { get; set; }
        public DPLStationSearchDetailModel search { get; set; }
    }

    public class DPLStationSearchDetailModel
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
}
