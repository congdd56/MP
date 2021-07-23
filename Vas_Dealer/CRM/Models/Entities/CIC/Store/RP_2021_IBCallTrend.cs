using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_IBCallTrend
    {
        [Key]
        public Int64 STT { get; set; }
        public string Time { get; set; }
        public int EnteredACD { get; set; }
        public int AnsweredACD { get; set; }
        public int AbandonedAcd { get; set; }
    }
}
