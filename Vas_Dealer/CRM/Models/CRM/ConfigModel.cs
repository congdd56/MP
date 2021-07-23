namespace VAS.Dealer.Models.CRM
{
    public class ConfigModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? IntValue { get; set; }
        public string StringValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}
