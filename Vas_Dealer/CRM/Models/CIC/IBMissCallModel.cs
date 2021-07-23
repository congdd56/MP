using MP.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class IBMisscallRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> Agent { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class IBMissCallIndexModel
    {
        public SelectList Agent { get; set; }
    }

    public class IBMissCallExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<IBMissCallResponseModel> ListItem { get; set; }
    }

    public class IBMissCallDetailResponseModel
    {
        public int STT { get; set; }
        public string CallId { get; set; }
        public string CallEventLog { get; set; }
        public string DNIS { get; set; }
        public string LocalUserId { get; set; }
        public string Line { get; set; }
        public string RemoteNumberFmt { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string InitiatedDateStr { get => InitiatedDate.ToString(MPFormat.DateTime_103Full); }
        public string Reason
        {
            get
            {
                if (CallEventLog.Contains("Remote Disconnect") && !CallEventLog.Contains("ACD interaction assigned"))
                    return "CG chưa đến nhân viên. Khách hàng ngắt máy";
                else if (CallEventLog.Contains("Remote Disconnect") && CallEventLog.Contains("ACD interaction assigned"))
                    return "CG đã đến nhân viên. Khách hàng ngắt máy";
                else if (CallEventLog.Contains("Local Disconnect") && !CallEventLog.Contains("ACD interaction assigned"))
                    return "CG chưa đến nhân viên. Hệ thống ngắt máy";
                else if (CallEventLog.Contains("Local Disconnect") && CallEventLog.Contains("ACD interaction assigned"))
                    return "CG đã đến nhân viên. Nhân viên ngắt máy";
                else if (!CallEventLog.Contains("Local Disconnect") && !CallEventLog.Contains("Remote Disconnect"))
                    return "Lý do khác";
                else
                    return "Lý do khác";
            }
        }
    }

    public class IBMissCallResponseModel
    {
        public int Counter { get; set; }
        public string Reason { get; set; }
        public List<IBMissCallDetailResponseModel> Details { get; set; }
    }
}
