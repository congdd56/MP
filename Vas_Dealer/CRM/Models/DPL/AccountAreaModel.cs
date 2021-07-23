using System.Collections.Generic;

namespace VAS.Dealer.Models.DPL
{
    public class AccountAreaModel
    {
        public int STT { get; set; }
        public string AreaId { get; set; }
        public string Area { get; set; }
        public List<string> TeamLeader { get; set; }
        public string TeamLeaderStr { get => string.Join(",", TeamLeader); }
        public List<string> UseName { get; set; }
        public string UseNameStr { get => string.Join(",", UseName); }
        public int AccountCounter { get; set; }
    }
}
