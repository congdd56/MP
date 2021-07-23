namespace VAS.Dealer.Models.CC
{
    public class BlindTransferActionModel
    {
        public string Channel { get; set; }
        public string Context { get; set; }
        public string Exten { get; set; }
    }
    public class AtxferActionModel
    {
        public string Channel { get; set; }
        public string Exten { get; set; }
        public string Context { get; set; }
    }
    public class MonitorActionModel
    {
        public string Channel { get; set; }
        public string File { get; set; }
        public string Format { get; set; }
        public bool Mix { get; set; }
    }
    public class ChanSpyModel
    {
        /// <summary>
        /// Tài khoản của SUP đi nghe xen
        /// </summary>
        public string SUPUserName { get; set; }
        /// <summary>
        /// Số nội bộ của SUP đi nghe xen
        /// </summary>
        public string SUPExtension { get; set; }
        /// <summary>
        /// Số nội bộ bị nghe xen
        /// </summary>
        public string Extension { get; set; }
        public string FixContext { get => "v9cc"; }
        public string FixExten { get => "101"; }
        public string FixPriority { get => "1"; }
    }

    public class OriginateActionModel
    {
        public string Channel { get; set; }
        public bool Async { get; set; }
        public string Data { get; set; }
        public string Application { get; set; }
        public string Priority { get; set; }
        public string Exten { get; set; }
        public string Context { get; set; }
        public string ChannelId { get; set; }
        public string Variable { get; set; }
        public string CallerId { get; set; }
        public string Account { get; set; }
        public string CompanyCode { get; set; }

    }
    public class QueueLogActionModel
    {
        public string Queue { get; set; }
        public string Event { get; set; }
        public string Uniqueid { get; set; }
        public string Interface { get; set; }
        public string Message { get; set; }
    }
    public class RedirectActionModel
    {
        public string Channel { get; set; }
        public string ExtraChannel { get; set; }
        public string Context { get; set; }
        public string Exten { get; set; }
        public int Priority { get; set; }
        public string CompanyCode { get; set; }

    }
}
