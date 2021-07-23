using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_Agent_Active
    {
        [Key]
        public Int64 STT { get; set; }
        public string Date { get; set; }
        public string AgentName { get; set; }

        public int LoggedIn { get; set; }
        public string LoggedInStr { get => TimeSpan.FromSeconds(LoggedIn).ToString(); }
        public int AvailableIB { get; set; }
        public string AvailableIBStr { get => TimeSpan.FromSeconds(AvailableIB).ToString(); }
        public int AvailableOB { get; set; }
        public string AvailableOBStr { get => TimeSpan.FromSeconds(AvailableOB).ToString(); }
        public int Aux { get; set; }
        public string AuxStr { get => TimeSpan.FromSeconds(Aux).ToString(); }
        public int CallHandlerTimeIB { get; set; }
        public string CallHandlerTimeIBStr { get => TimeSpan.FromSeconds(CallHandlerTimeIB).ToString(); }
        public int CallHandlerTimeOB { get; set; }
        public string CallHandlerTimeOBStr { get => TimeSpan.FromSeconds(CallHandlerTimeOB).ToString(); }
        public int HandlingIB { get; set; }
        public string HandlingIBStr { get => TimeSpan.FromSeconds(HandlingIB).ToString(); }
        public int HandlingOB { get; set; }
        public string HandlingOBStr { get => TimeSpan.FromSeconds(HandlingOB).ToString(); }
        public int HoldTimeIB { get; set; }
        public string HoldTimeIBStr { get => TimeSpan.FromSeconds(HoldTimeIB).ToString(); }
        public int HoldTimeOB { get; set; }
        public string HoldTimeOBStr { get => TimeSpan.FromSeconds(HoldTimeOB).ToString(); }
        public int TimeWatingib { get; set; }
        public string TimeWatingibStr { get => TimeSpan.FromSeconds(TimeWatingib).ToString(); }
        public int TimeWatingob { get; set; }
        public string TimeWatingobStr { get => TimeSpan.FromSeconds(TimeWatingob).ToString(); }
        public int Meeting { get; set; }
        public string MeetingStr { get => TimeSpan.FromSeconds(Meeting).ToString(); }
        public int Training { get; set; }
        public string TrainingStr { get => TimeSpan.FromSeconds(Training).ToString(); }
        public int AgentAvailTimeIB { get; set; }
        public string AgentAvailTimeIBStr { get => TimeSpan.FromSeconds(AgentAvailTimeIB).ToString(); }
        public int AgentAvailTimeOB { get; set; }
        public string AgentAvailTimeOBStr { get => TimeSpan.FromSeconds(AgentAvailTimeOB).ToString(); }
        public int AgentAvailTime { get; set; }
        public string AgentAvailTimeStr { get => TimeSpan.FromSeconds(AgentAvailTime).ToString(); }

        public double PerformAgentAnswerIB { get; set; }
        public string PerformAgentAnswerIBStr { get => string.Format("{0:0.00}", PerformAgentAnswerIB); }
        public double PerformAgentAnswerOB { get; set; }
        public string PerformAgentAnswerOBStr { get => string.Format("{0:0.00}", PerformAgentAnswerOB); }
        public double PerformAgentIB { get; set; }
        public string PerformAgentIBStr { get => string.Format("{0:0.00}", PerformAgentIB); }
        public double PerformAgentOB { get; set; }
        public string PerformAgentOBStr { get => string.Format("{0:0.00}", PerformAgentOB); }

        public int Total { get; set; }
    }
}
