using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace CDFAPI.Repository
{
    public class DataContex
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Send mail function
        public Boolean SendEmail(string recepientEmail, string subject, string body)
        {
            try
            {
                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return false;
            }
        }

        //Send SMS Function
        public Boolean sendSmsOld(string mob, string msg)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string userid = ConfigurationManager.AppSettings["SMSUserId"].ToString();  //  "2000167436";
                string passwd = ConfigurationManager.AppSettings["SMSPassword"].ToString();  //  "xzreMXXv5";
                string url =
            "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" +
            mob + "&msg=" + msg + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";

                request = WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                return false;
            }
        }

  
        public Boolean sendemail(string to, string cc, string bcc, string subject, string body)
        {
            try
            {
                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    if (to != null)
                        mailMessage.To.Add(new MailAddress(to));
                    if (cc != null)
                        mailMessage.CC.Add(new MailAddress(cc));
                    if (bcc != null)
                        mailMessage.Bcc.Add(new MailAddress(bcc));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mailMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public Boolean sendSms(string mob, string msg)
        {
            try
            {
                //String message = HttpUtility.UrlEncode("Hello,This is testing SMS.");
                using (var wb = new System.Net.WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
               {
               {"apikey" , "+xUuNO0Cn/k-MntgP04VgfPf5I8ukT5TO3sndXrFEP"},
               {"numbers" , mob},
               {"message" , msg},
               {"sender" , "DHEYAC"}
               });
                    string result = System.Text.Encoding.UTF8.GetString(response);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log.Error("" + ex);
                return false;
            }
        }
    }
}