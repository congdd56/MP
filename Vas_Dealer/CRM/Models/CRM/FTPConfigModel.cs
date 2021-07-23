using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Models.CRM
{
    public class FTPConfigModel
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PassiveMode { get; set; }
        /// <summary>
        /// Tiền tố của file ftp
        /// </summary>
        public string PrefixRegis30 { get; set; }
        public string PrefixRegis1Day { get; set; }
        public string PrefixRenew1Day { get; set; }

        public string PrefixRegis30Schedule { get; set; }
        public string PrefixRegis1DaySchedule { get; set; }
        public string PrefixRenew1DaySchedule { get; set; }

        /// <summary>
        /// Đường dẫn tải về từ ftp
        /// </summary>
        public string DownloadPath { get; set; }
    }
}
