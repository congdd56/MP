using System;

namespace VAS.Dealer.Models.Entities.CIC
{
    public class CC_CallDetail
    {
        public string CallId { get; set; }
        public string CallType { get; set; }
        public string CallDirection { get; set; }
        public string LineId { get; set; }
        public string StationId { get; set; }
        public string LocalUserId { get; set; }
        public string LocalNumber { get; set; }
        public string LocalName { get; set; }
        public string RemoteNumber { get; set; }
        public string RemoteNumberLoComp1 { get; set; }
        public string RemoteNumberLoComp2 { get; set; }
        public string RemoteNumberFmt { get; set; }
        public string RemoteNumberCallId { get; set; }
        public string RemoteName { get; set; }
        public DateTime InitiatedDate { get; set; }
        public DateTime? InitiatedDateTimeGmt { get; set; }
        public DateTime? ConnectedDate { get; set; }
        public DateTime? ConnectedDateTimeGmt { get; set; }
        public DateTime TerminatedDate { get; set; }
        public DateTime? TerminatedDateTimeGmt { get; set; }
        public int? CallDurationSeconds { get; set; }
        public int? HoldDurationSeconds { get; set; }
        public int? LineDurationSeconds { get; set; }
        public string DNIS { get; set; }
        public string CallEventLog { get; set; }
        public string AccountCode { get; set; }
    }

    public class CC_RecordingCall
    {
        public string RECORDINGID { get; set; }
        public string RecordedCallIDKey { get; set; }
    }

    public class CC_RecordingData
    {
        public string RecordingId { get; set; }
        public string RecordingFileName { get; set; }
    }
}
