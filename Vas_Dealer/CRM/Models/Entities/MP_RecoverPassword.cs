using System;

namespace VAS.Dealer.Models.Entities
{
    public class MP_RecoverPassword
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Totp { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSent { get; set; }
        public bool SendSuccess { get; set; }
        public bool IsHandler { get; set; }
        public DateTime? HandlerDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
