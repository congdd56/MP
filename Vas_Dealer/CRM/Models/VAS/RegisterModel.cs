using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;

namespace VAS.Dealer.Models.VAS
{
    public class RegisterIndeModel
    {
        public List<Select2Model> Vendors { get; set; }
        public List<Select2Model> Services { get; set; }
    }
}
