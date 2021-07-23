using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace MP.Common
{
    public static class MPHelper
    {


        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }
        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }

        #region Fixed
        public static string Conflict = "Đừng yêu em nữa";
        public static string Department_CallCenter = "DEP01";
        #endregion

        public static string GetStatusTextCS(VOC_Tab tab)
        {
            switch (tab)
            {
                case VOC_Tab.CallCenter:
                    return "CC-Request";
                case VOC_Tab.Discus:
                    return "Ad-Request";
                case VOC_Tab.Tech:
                    return "Te-Request";
                default:
                    return "";
            }
        }
        public static string GetTime2Date(DateTime? fDate, DateTime? tDate)
        {
            if (!fDate.HasValue || !tDate.HasValue) return string.Empty;
            TimeSpan span = (tDate.Value - fDate.Value);
            return String.Format("{0} ngày, {1} giờ, {2} phút", span.Days, span.Hours, span.Minutes);
        }

        /// <summary>
        /// Lấy extension file theo đường dẫn
        /// </summary>
        /// <param name="attachFileName"></param>
        /// <returns></returns>
        public static string GetExtenFileAttachByPath(string attachFileName)
        {
            FileInfo fi = new FileInfo(attachFileName);
            return fi.Extension;
        }
        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public static string GetNameOfEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return "";
            string[] arrEmail = email.Split('@');
            if (arrEmail != null && arrEmail.Length > 1)
            {
                return arrEmail[0];
            }
            return email;
        }

        /// <summary>
        /// Gửi email
        /// </summary>
        /// <param name="SSL"></param>
        /// <param name="email"></param>
        /// <param name="emailPasss"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="emailTo"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="sendMailMessage"></param>
        /// <param name="nameOfEmail"></param>
        /// <param name="nameOfToEmail"></param>
        /// <param name="attachmentFilename"></param>
        /// <returns></returns>
        public static bool SendMail(bool SSL, string email, string emailPasss, string host, int port, string emailTo,
            string subject, string content, out string sendMailMessage,
            string nameOfEmail = "", string nameOfToEmail = "", string attachmentFilename = "")
        {
            sendMailMessage = string.Empty;
            try
            {

                var fromAddress = new MailAddress(email, nameOfEmail);
                var toAddress = new MailAddress(emailTo, nameOfToEmail);
                string fromPassword = Decrypt(emailPasss);


                //string userEmail = GetNameOfEmail(email);
                string userEmail = email;

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = SSL,
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
                if (!string.IsNullOrEmpty(attachmentFilename))
                    message.Attachments.Add(new Attachment(attachmentFilename));

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
                sendMailMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Lấy số tuần hiện tại trong năm
        /// </summary>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear()
        {
            DateTime time = DateTime.Now;
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static string Decrypt(string cipherString, string key = "mptelecom", bool useHashing = true)
        //public static string Decrypt(string cipherString, string key = "mptelecom@#2019*ITxOTC", bool useHashing = true)
        {
            byte[] keyArray;
            //get the byte code of the string


            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string Encrypt(string toEncrypt, string key = "mptelecom", bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }

    public static class MPFormat
    {
        /// <summary>
        /// Định dạng thời gian M/yyyy
        /// </summary>
        public static string DateTime_Myyyy = "M/yyyy";
        /// <summary>
        /// Định dạng thời gian MM/yyyy
        /// </summary>
        public static string DateTime_MMyyyy = "MM/yyyy";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy
        /// </summary>
        public static string DateTime_103 = "dd/MM/yyyy";
        /// <summary>
        /// Định dạng thời gian MM/dd/yyyy
        /// </summary>
        public static string DateTime_101 = "MM/dd/yyyy";
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public static string DateTime_121 = "yyyy-MM-dd";
        /// <summary>
        /// yyyy-MM-dd HH:mm:00:000
        /// </summary>
        public static string yyyyMMddHHmm00000 = "yyyy-MM-dd HH:mm:00:000";
        /// <summary>
        /// yyyy-MM-dd HH:mm:59:998
        /// </summary>
        public static string yyyyMMddHHmm59998 = "yyyy-MM-dd HH:mm:59:998";
        /// <summary>
        /// Định dạng yyyy-MM-dd
        /// </summary>
        public static string DateTime_121HHmm = "yyyy-MM-dd HH:mm";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy HH:mm
        /// </summary>
        public static string DateTime_ddMMyyyyHHmm = "dd/MM/yyyy HH:mm";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy 00:00
        /// </summary>
        public static string DateTime_ddMMyyyyHHmm_FirstTime = "dd/MM/yyyy 00:00";
        /// <summary>
        /// Định dạng thời gian dd/MM/yyyy 23:59
        /// </summary>
        public static string DateTime_ddMMyyyyHHmm_LastTime = "dd/MM/yyyy 23:59";
        /// <summary>
        /// dd/MM/yyyy HH:mm:ss
        /// </summary>
        public static string DateTime_103Full = "dd/MM/yyyy HH:mm:ss";
        /// <summary>
        /// dd/MM/yyyy HH:mm:ss.fff
        /// </summary>
        public static string ddMMyyyyHHmmssfff = "dd/MM/yyyy HH:mm:ss.fff";
        /// <summary>
        /// dd/MM/yyyy HH:mm:00.000
        /// </summary>
        public static string ddMMyyyyHHmmssfff_FistTime = "dd/MM/yyyy HH:mm:00.000";
        /// <summary>
        /// dd/MM/yyyy HH:mm:23.998
        /// </summary>
        public static string ddMMyyyyHHmmssfff_lastTime = "dd/MM/yyyy HH:mm:23.998";
        /// <summary>
        /// yyyyMMdd
        /// </summary>
        public static string yyyyMMdd = "yyyyMMdd";
        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        public static string yyyyMMddHHmmss = "yyyyMMddHHmmss";

        /// <summary>
        /// ddMMyyyy HHmmss
        /// </summary>
        public static string Exten_ddMMyyyyHHmmss = "ddMMyyyy HHmmss";
    }

}
