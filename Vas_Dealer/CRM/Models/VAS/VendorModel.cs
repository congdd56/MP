using MP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.VAS
{
    public class VendorResponseModel
    {
        public int STT { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhoneNo84
        {
            get => (!string.IsNullOrEmpty(Phone) && Phone.Length > 1) ? Phone.Substring(2, Phone.Length - 2) : Phone;
        }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get => CreatedDate.ToString(MPFormat.DateTime_103Full); }
        public string CreatedBy { get; set; }
    }
}
