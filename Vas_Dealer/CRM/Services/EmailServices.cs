using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class EmailServices : IEmailServices
    {

        private readonly ILogger<EmailServices> _logger;
        private readonly FTPConfigModel _FTPConfigModel;
        private readonly UserTokenSettingModel _UserTokenSetting;
        private readonly MP_Context _Context;
        public EmailServices(ILogger<EmailServices> logger, MP_Context Context, IOptions<FTPConfigModel> FTPConfig,
            IOptions<UserTokenSettingModel> UserTokenSetting)
        {
            _logger = logger;
            _Context = Context;
            _FTPConfigModel = FTPConfig.Value;
            _UserTokenSetting = UserTokenSetting.Value;
        }




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
        public bool SendMail(bool SSL, string userEmail, string email, string host, int port, string emailTo,
            string subject, string content, out string sendMailMessage,
            string nameOfEmail = "", string nameOfToEmail = "")
        {
            sendMailMessage = string.Empty;
            try
            {
                var fromAddress = new MailAddress(email, nameOfEmail);
                var toAddress = new MailAddress(emailTo, nameOfToEmail);
                string fromPassword = _UserTokenSetting.EmailPasswordMailSend;



                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(userEmail, fromPassword)
                };

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = content,
                    IsBodyHtml = true,
                };
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object s, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                    smtp.Send(message);
                }
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                sendMailMessage = ex.Message;
                return false;
            }
        }


        public void SendWarningFtpError(string exception)
        {
            try
            {
                var fromAddress = new MailAddress("notify@mptelecom.com.vn", "MP Notify");
                var toAddress = new MailAddress(_UserTokenSetting.EmailReceive, "MP TrucCa HNI");

                var smtp = new SmtpClient
                {
                    Host = "192.168.22.19",
                    Port = 587,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("notify@mptelecom.com.vn", _UserTokenSetting.EmailPasswordMailSend)
                };

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "[VAS_Dealer] Cảnh báo không kết nối được ftp nhà mạng.",
                    Body = exception + "<br/>Xem lại file cấu hình appsetting.json trong thư mục source code",
                    IsBodyHtml = true,
                };
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object s, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                    smtp.Send(message);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

    }
}
