using VAS.Dealer.Models.Entities.CIC.Store;
using System;
using System.Collections.Generic;

namespace VAS.Dealer.Models.CIC
{
    public class AgentLoginResponseModel
    {
        public int STT { get; set; }

        #region CIC
        public string Day { get; set; }
        public string Agent { get; set; }
        public int? Answer { get; set; }
        public string LoginTime { get; set; }
        public string AnswerTime { get; set; }
        public string NotAvaiable { get; set; }
        public string Avaiable { get; set; }
        #endregion

        #region CRM
        public string LogDate { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Logout { get; set; }
        #endregion
    }

    public class AgentLoginRequestModel
    {
        public string Agents { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class AgentLoginCRMModel
    {
        public int STT { get; set; }
        public string UserName { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime Login { get; set; }
        public DateTime? Logout { get; set; }
    }
    public class AgentLoginExportModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<RP_2021_AGENT_CRMLogin> ListItem { get; set; }
    }



}
