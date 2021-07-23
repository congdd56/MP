using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.VOC.Report
{

    public class CSRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Account { get; set; }

    }

    public class CSResponseModel
    {
        public int STT { get; set; }
        public string Account { get; set; }
        public DateTime CreatedDate { get; set; }
        public double? CountCS { get; set; }
        public double CountSuccess { get; set; }
        public string RateSuccess { get => (String.Format("{0:0.##}", CountSuccess * 100 / CountCS)); }
        public double CountUnsuccess { get; set; }
        public string RateUnsuccess { get => (String.Format("{0:0.##}", CountUnsuccess * 100 / CountCS)); }
    }
    public class CSExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<CSResponseModel> Details { get; set; }
    }
}
