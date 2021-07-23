using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.Entities
{
    public class MP_EmailConfig
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public string PortServer { get; set; }
        public bool SSLoTLS { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
