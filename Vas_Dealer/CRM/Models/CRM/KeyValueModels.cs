using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class KeyValueStringDatetimeModels
    {
        public string Key { get; set; }
        public DateTime Value { get; set; }
    }

    public class PermisTeamleaderModel
    {
        public string Area { get; set; }
        public string UserName { get; set; }
    }

    public class Select2Model
    {
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning disable IDE1006 // Naming Styles
        public string text { get; set; }
#pragma warning restore IDE1006 // Naming Styles
        public bool selected { get; set; }
        public string group { get; set; }
        public string des { get; set; }
    }

    public class KeyValueIntStringModels
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class KeyValueBoolStringModel
    {
        public bool Key { get; set; }
        public List<string> Value { get; set; }
    }

    public class KeyValueStringStringModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
