using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_AGENT_CRMLogin
    {
        [Key]
        public Int64 STT { get; set; }
        public string Day { get; set; }
        public string Agent { get; set; }
        public DateTime? Login { get; set; }
        public string LoginStr { get => Login.HasValue ? Login.Value.ToString(MPFormat.DateTime_103Full) : string.Empty; }
        public string DateLogOut { get; set; }
        public DateTime? Logout { get; set; }
        public string LogoutStr { get => Logout.HasValue ? Logout.Value.ToString(MPFormat.DateTime_103Full) : string.Empty; }
        public int? Answer { get; set; }
        public string LoginTime { get; set; }
        public string AnswerTime { get; set; }
        public string NotAvaiable { get; set; }
        public string Avaiable { get; set; }
    }
}
