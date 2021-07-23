using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class RoleInfoModel
    {
        public string Name { get; set; }
        public int IdPer { get; set; }
        public List<Select2Model> AccountManager { get; set; }
    }
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }
        public string CountAgent { get; set; }
        public string Manager { get; set; }
    }
}
