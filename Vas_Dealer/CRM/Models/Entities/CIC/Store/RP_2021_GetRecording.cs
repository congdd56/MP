using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_GetRecording
    {
        [Key]
        public Int64 STT { get; set; }
        public string CallId { get; set; }
        public string RemoteNumberFmt { get; set; }
        public DateTime InitiatedDate { get; set; }
        public virtual string InitiatedDateStr { get => InitiatedDate.ToString(MPFormat.DateTime_103Full); }
        public string LineId { get; set; }
        public int RecordingLength { get; set; }
        public int RecordingFileSize { get; set; }
        public DateTime RecordingDate { get; set; }
        public virtual string RecordingDateStr { get => RecordingDate.ToString(MPFormat.DateTime_103Full); }
        public DateTime TerminatedDate { get; set; }
        public virtual string TerminatedDateStr { get => TerminatedDate.ToString(MPFormat.DateTime_103Full); }
        public string StationId { get; set; }
        public string CallType { get; set; }
        public string CallDirection { get; set; }
        public string LocalUserId { get; set; }
        public string LocalNumber { get; set; }
        public string Disconnect { get; set; }
        public string RecordingFileName { get; set; }
        public string RecordingId { get; set; }
        public int CallDurationSeconds { get; set; }
    }
}
