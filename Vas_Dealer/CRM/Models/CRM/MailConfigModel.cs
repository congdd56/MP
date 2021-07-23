using MP.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace VAS.Dealer.Models.CRM
{
    public class MailConfigModel
    {
        public int Id { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public string PortServer { get; set; }
        [Required]
        public bool SSLoTLS { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Encode { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UserOnlineModel
    {
        public int STT { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime LoginDate { get; set; }
        public string LoginDateStr { get => LoginDate.ToString(MPFormat.DateTime_ddMMyyyyHHmm); }
    }
}
