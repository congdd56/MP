using MP.Common;
using VAS.Dealer.Models.Entities.CIC.Store;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class OBTotalRequestModel
    {
        public string Agents { get; set; }
        public string Phone { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class OBTotalDataExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<OBTotalData> ListItem { get; set; }

    }

    public class OBDetailsExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_CIC_2021_OBDetails> ListItem { get; set; }
    }

    public class OBTotalData
    {
        public Int64 STT { get; set; }
        public string CallId { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string InitiatedDateStr { get => InitiatedDate.ToString(MPFormat.DateTime_103Full); }
        public DateTime? ConnectedDate { get; set; }
        public string ConnectedDateStr { get => (ConnectedDate.HasValue && ConnectedDate.Value > new DateTime(1980, 1, 1)) ? ConnectedDate.Value.ToString(MPFormat.DateTime_103Full) : string.Empty; }
        public string LocalUserId { get; set; }
        public string RecordingFileName { get; set; }
        public string RemoteNumberFmt { get; set; }
        public DateTime TerminatedDate { get; set; }
        public string TerminatedDateStr { get => TerminatedDate.ToString(MPFormat.DateTime_103Full); }
        public string RECORDINGID { get; set; }
        public virtual string Status { get => string.IsNullOrEmpty(RECORDINGID) ? "Không kết nối" : "Kết nối"; }
        public virtual string StatusDetails { get => GetStatusDetail(RECORDINGID, CallEventLog, CallDurationSeconds.Value); }
        public string CallEventLog { get; set; }
        public int? LineDurationSeconds { get; set; }
        public int? CallDurationSeconds { get; set; }
        public virtual int WaittingTime { get => LineDurationSeconds.Value - CallDurationSeconds.Value; }
        public virtual int TotalTime { get => (LineDurationSeconds.Value - CallDurationSeconds.Value) + CallDurationSeconds.Value; }

        string GetStatusDetail(string RECORDINGID, string CallEventLog, int CallDurationSeconds)
        {
            if (string.IsNullOrEmpty(RECORDINGID)) return "Cuộc gọi không kết nối được do khóa máy/ngoài vùng phủ sóng";

            if (CallEventLog.Contains("Remote Busy")) return "Bận máy";
            else if (CallEventLog.Contains("No User Responding") || (CallEventLog.Contains("Local Hang Up") && CallDurationSeconds == 0)) return "Không bắt máy";
            else if (CallEventLog.Contains("Invalid Number Format")) return "Thuê bao không đúng";
            else return "Kết nối";
        }
    }
}
