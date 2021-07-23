using System.Collections.Generic;

namespace VAS.Dealer.Models.CC
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsLock { get; set; }
        public string PassWord { get; set; }
        public bool IsActive { get; set; }
        public string ListRole { get; set; }
        /// <summary>
        /// Hiện thị trạng thái online offline theo signalr
        /// </summary>
        public bool SignalStatus { get; set; }
        public string ListDepartment { get; set; }
        public List<string> Roles { get; set; }
        public int Vendor { get; set; }
        public string VendorStr { get; set; } 
        public bool IsDeleted { get; set; }
        public bool IsPriviot { get; set; }
    }

    public class ChangePasswordModel
    {
        public int Id { get; set; }
        public string passwordOld { get; set; }
        public string passwordNew { get; set; }
    }
}
