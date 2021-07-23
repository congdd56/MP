using VAS.Dealer.Models.Entities.CIC.Store;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class MisscallRequestModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> Agent { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class MissCallIndexModel
    {
        public SelectList Agent { get; set; }
    }

    public class MissCallExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<AgentMissCallResponseModel> ListItem { get; set; }
    }

    public class AgentMissCallDetailResponseModel
    {
        /// <summary>
        /// Mã cuộc gọi
        /// </summary>
        public string CallId { get; set; }
        public string CallEventLog { get; set; }
        public string DNIS { get; set; }
        public string LocalUserId { get; set; }
        public string Line { get; set; }
        public string RemoteNumberFmt { get; set; }
        public DateTime InitiatedDate { get; set; }
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

    public class AgentMissCallResponseModel
    {
        public int Counter { get; set; }
        public string Reason { get; set; }
        public List<AgentMissCallDetailResponseModel> Details { get; set; }
    }
}
