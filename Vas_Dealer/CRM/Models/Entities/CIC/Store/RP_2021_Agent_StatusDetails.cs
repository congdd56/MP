using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_Agent_StatusDetails
    {
        [Key]
        public Int64 STT { get; set; }
        public string UserId { get; set; }
        public string StatusKey { get; set; }
        public string InDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string LoginStatus { get; set; }
    }
}
