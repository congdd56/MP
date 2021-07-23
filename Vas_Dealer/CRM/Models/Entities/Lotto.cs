using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.VAS
{
    public class Lotto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Score { get; set; } 
        public DateTime UpdatedDate { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsClosed { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class LottoModel2
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountUserName { get; set; }
        public string AccountFullName { get; set; }
        public int Score { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsClosed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
