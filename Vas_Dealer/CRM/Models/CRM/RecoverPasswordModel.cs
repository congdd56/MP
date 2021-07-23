namespace VAS.Dealer.Models.CRM
{
    public class RecoverPasswordModel
    {
        public string UserName { get; set; }
        public string Otp { get; set; }
        public string Msg { get; set; }
    }
}
