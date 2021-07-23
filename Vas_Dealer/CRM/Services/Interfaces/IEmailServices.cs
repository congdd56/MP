using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Services.Interfaces
{
    public interface IEmailServices
    {
        /// <summary>
        /// Gửi email
        /// </summary>
        /// <param name="SSL"></param>
        /// <param name="userEmail"></param>
        /// <param name="email"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="emailTo"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="sendMailMessage"></param>
        /// <param name="nameOfEmail"></param>
        /// <param name="nameOfToEmail"></param>
        /// <returns></returns>
        bool SendMail(bool SSL, string userEmail, string email, string host, int port, string emailTo,
           string subject, string content, out string sendMailMessage,
           string nameOfEmail = "", string nameOfToEmail = "");
        void SendWarningFtpError(string exception);
    }
}
