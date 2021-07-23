namespace VAS.Dealer.Models.CRM
{
    public class SendMailConfigModel
    {
        public string SMTP { get; set; }
        public string MailAddress { get; set; }
        public string Port { get; set; }
        public string ToMail { get; set; }
        public string CCMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
