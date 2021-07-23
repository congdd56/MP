using System;

namespace VAS.Dealer.Models.Entities.DPL
{
    public class DPL_AuthenProperty
    {
        public int Id { get; set; }
        public bool Authenticated { get; set; }
        public string Timestamp { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
