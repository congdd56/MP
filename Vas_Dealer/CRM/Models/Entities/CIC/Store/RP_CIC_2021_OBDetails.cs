using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_CIC_2021_OBDetails
    {
        [Key]
        public Int64 STT { get; set; }
        public string Indate { get; set; }
        public string LocalUserId { get; set; }
        public int Totals { get; set; }
        public int CallConected { get; set; }
        public int TotalDisconect { get; set; }
        public virtual int TotalNotConnection { get => TotalDisconect - CallNotPickUp - Busy - DisconectByWrong; }
        public int CallNotPickUp { get; set; }
        public int Busy { get; set; }
        public int DisconectByWrong { get; set; }
        public virtual decimal ConnectionPercent { get => (CallConected / Totals) * 100; }
        public int TotalTime { get; set; }
        public int TotalWaittime { get; set; }
        public int CallDurationTime { get; set; }
        public double AVGCallDuration { get; set; }
        public int CountCall10 { get; set; }
    }
}
