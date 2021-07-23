using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VAS.Dealer.Models.Entities.CIC.Store
{
    public class RP_2021_AGENT_CICLogin
    {
        [Key]
        public Int64 STT { get; set; }
        [Column("Thời gian")]
        public string Day { get; set; }
        [Column("Nhân viên")]
        public string Agent { get; set; }
        [Column("Cuộc gọi trả lời")]
        public int? Answer { get; set; }
        [Column("Thời gian đăng nhập")]
        public string LoginTime { get; set; }
        [Column("Thời gian trả lời")]
        public string AnswerTime { get; set; }
        [Column("Thời gian không sẵn sàng")]
        public string NotAvaiable { get; set; }
        [Column("Thời gian sẵn sàng")]
        public string Avaiable { get; set; }
    }
}
