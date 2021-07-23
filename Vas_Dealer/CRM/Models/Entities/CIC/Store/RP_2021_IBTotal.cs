using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_IBTotal
    {
        [Key]
        public Int64 STT { get; set; }
        public string HourTime { get; set; }
        public int ConnectQueue { get; set; }
        public int ConnectedCall { get; set; }
        public int? AvgCallDuration { get; set; }
        public int MissCall { get; set; }
        public double ProportionMiss { get; set; }
        public string ProportionMissStr { get => ProportionMiss.ToString("0.00"); }
        public double ProportionConnected { get; set; }
        public string ProportionConnectedStr { get => ProportionConnected.ToString("0.00"); }
    }
}
