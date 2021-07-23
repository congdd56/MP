using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VAS.Dealer.Models.Entities
{
    public class MP_Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int PasswordFormat { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? VendorId { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MP_AccountRole> AccountRole { get; set; }
        public virtual VAS_Vendor Vendor { get; set; }  
        public bool IsPriviot { get; set; }
    }

    public class MP_AccountLoginTime
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime LogTime { get; set; }
    }
}
