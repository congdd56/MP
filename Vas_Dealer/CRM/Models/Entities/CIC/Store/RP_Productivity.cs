using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    /// <summary>
    /// Báo cáo năng suất điện thoại viên
    /// </summary>
    public class RP_Agent_Productivity
    {
        [Key]
        public Int64 Id { get; set; }
        public string AgentName { get; set; }
        public int TotalHandling { get; set; }
        public int In10s { get; set; }
        public int TalkTimeOn240s { get; set; }
        public double RateTalkTimeOn240s { get; set; }
        public virtual string RateTalkTimeOn240sStr { get => string.Format("{0:0.00}%", RateTalkTimeOn240s); }
        public int AvgHandlingTime { get; set; }
        public virtual TimeSpan AvgHandlingTimeStr { get => TimeSpan.FromSeconds(AvgHandlingTime); }
        public int AvgTalkTime { get; set; }
        public virtual TimeSpan AvgTalkTimeStr { get => TimeSpan.FromSeconds(AvgTalkTime); }
        public int AvgHoldTime { get; set; }
        public virtual TimeSpan AvgHoldTimeStr { get => TimeSpan.FromSeconds(AvgHoldTime); }
        public int MinHoldTime { get; set; }
        public virtual TimeSpan MinHoldTimeStr { get => TimeSpan.FromSeconds(MinHoldTime); }
        public int MaxHoldTime { get; set; }
        public virtual TimeSpan MaxHoldTimeStr { get => TimeSpan.FromSeconds(MaxHoldTime); }
        public int TotalOutBoundTime { get; set; }
        public virtual TimeSpan TotalOutBoundTimeStr { get => TimeSpan.FromSeconds(TotalOutBoundTime); }
        public int TotalOutAndIn { get; set; }
        public double PerformAgentAnswer { get; set; }
        public virtual string PerformAgentAnswerStr { get => string.Format("{0:0.00}", PerformAgentAnswer); }
        public double PerformAgent { get; set; }
        public virtual string PerformAgentStr { get => string.Format("{0:0.00}", PerformAgent); }
        public double RateHandlingIn10s { get; set; }
        public virtual string RateHandlingIn10sStr { get => string.Format("{0:0.00}%", RateHandlingIn10s); }
    }

    public class RP_Agent_MissCall
    {
        [Key]
        public Int64 Id { get; set; }
        public string RemoteNumberFmt { get; set; }
        public DateTime? InitiatedDateMiss { get; set; }
        public string InitiatedDateMissStr { get => InitiatedDateMiss.HasValue ? InitiatedDateMiss.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? TerminatedDateMiss { get; set; }
        public string TerminatedDateMissStr { get => TerminatedDateMiss.HasValue ? TerminatedDateMiss.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public DateTime? InitiatedDateCall { get; set; }
        public string InitiatedDateCallStr { get => InitiatedDateCall.HasValue ? InitiatedDateCall.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public int DurationCall { get; set; }
        public string AgentCall { get; set; }
        public DateTime? InitiatedDateCallOutbound { get; set; }
        public string InitiatedDateCallOutboundStr { get => InitiatedDateCallOutbound.HasValue ? InitiatedDateCallOutbound.Value.ToString(MPFormat.DateTime_ddMMyyyyHHmm) : string.Empty; }
        public int DurationCallOutbound { get; set; }
    }
    public class RP_IB_Productivity
    {
        [Key]
        public Int64 Id { get; set; }
        public string InitiatedDate { get; set; }
        public string AgentName { get; set; }
        public Int32 TotalHandling { get; set; }
        public Int32 In10s { get; set; }
        public Int32 TalkTimeOn240s { get; set; }
        public double RateTalkTimeOn240s { get; set; }
        public Int32 AvgHandlingTime { get; set; }
        public Int32 AvgTalkTime { get; set; }
        public Int32 AvgHoldTime { get; set; }
        public double PerformAgentAnswer { get; set; }
        public string PerformAgentAnswerStr { get => PerformAgentAnswer.ToString("C2"); }
        public double PerformAgent { get; set; }
        public string PerformAgentStr { get => PerformAgent.ToString("C2"); }
        public double RateHandlingIn10s { get; set; }
        public string RateHandlingIn10sStr { get => RateHandlingIn10s.ToString("C2"); }

    }
}
