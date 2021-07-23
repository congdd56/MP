using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class ReportIBCallDetails
    { 
        [Key]
        public int WaitAVR { get; set; } 
        public virtual string WaitAVRStr { get => string.Format("{0:0.00}", WaitAVR); } 
        public int TalkTime { get; set; }
        public virtual string TalkTimeStr { get => string.Format("{0:0.00}", TalkTime); }
        public string CallEventLog { get; set; }
        public string dnis { get; set; }
        public string Date { get; set; }
        public string CallId { get; set; } 
        public string dnis2 { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string UserId { get; set; }
        public string StartTime { get; set; }
        public string RemoteNumberFmt { get; set; }
        public string PickUp { get; set; }
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public string RemoteName { get; set; } 
        public string PickUpTime { get; set; }
        public int WaitTime { get; set; }
        public virtual string WaitTimeStr { get => string.Format("{0:0.00}", WaitAVR); }
        public int CallDurationTime { get; set; }
        public virtual string CallDurationTimeStr { get => string.Format("{0:0.00}", CallDurationTime); }
        public string EndTime { get; set; }
        public string Disconnect { get; set; }
        public string groupname { get; set; }
        public string DirTran { get; set; }
        public string RecordingFileName { get; set; }
    }
    public class ReportIBCallDetailsModel
    {
        public int STT { get; set; } 
        public int WaitAVR { get; set; }
        public string WaitAVRStr { get; set; }
        public int TalkTime { get; set; }
        public string TalkTimeStr { get; set; }
        public string CallEventLog { get; set; }
        public string dnis { get; set; }
        public string Date { get; set; }
        public string CallId { get; set; }
        public string dnis2 { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string UserId { get; set; }
        public string StartTime { get; set; }
        public string RemoteNumberFmt { get; set; }
        public string PickUp { get; set; }
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public string RemoteName { get; set; }
        public string PickUpTime { get; set; }
        public int WaitTime { get; set; }
        public string WaitTimeStr { get; set; }
        public int CallDurationTime { get; set; }
        public string CallDurationTimeStr { get; set; }
        public string EndTime { get; set; }
        public string Disconnect { get; set; }
        public string groupname { get; set; }
        public string DirTran { get; set; }
        public string RecordingFileName { get; set; }
    }
    public class ReportCallDetailsIBExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<ReportIBCallDetailsModel> ListItem { get; set; }
    }
}
