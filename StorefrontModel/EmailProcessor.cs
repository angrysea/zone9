using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;

namespace StorefrontModel
{
    class EmailProcessor
    {
        private Entities context;
        public HttpWebResponse HttpWResponse { get; private set; }
        public HttpWebRequest HttpWRequest{ get; private set; }
        public bool userProxy { get; set; }
        public bool useIESettings { get; set; }
        public string proxyServer { get; set; }
        public string proxyUser { get; set; }
        public string proxyPassword { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public EmailProcessor(Entities context)
        {
            this.context = context;
        }

        public void SendEmail(string email, string toAddress, string subject, string id)
        {
            MailConfiguration mailconfig = context.MailConfiguration.Where(email);
            SmtpClient client = new SmtpClient(mailconfig.MailHost);
            MailAddress from = new MailAddress(mailconfig.MailFromAddress);
            MailAddress to = new MailAddress(toAddress);
            MailMessage message = new MailMessage(from, to);
            message.Subject = mailconfig.MailSubject + subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = MakeWebRequest(mailconfig.MailURL);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userState = "message1";
            client.SendAsync(message, userState);

        }

        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;
        }

        public string MakeWebRequest(string url)
        {
            string response;

            Uri URL = new Uri(url);
            if (userProxy)
            {
                if (proxyServer.Length < 1)
                {
                    return null;
                }
                
                WebProxy proxyObject = null;

                if (useIESettings)
                {
                    proxyObject = WebProxy.GetDefaultProxy();
                }
                else
                {
                    proxyObject = new WebProxy(proxyServer, true);
                    proxyObject.Credentials = new NetworkCredential(proxyUser, proxyPassword);
                }
                GlobalProxySelection.Select = proxyObject;
            }

            HttpWRequest = (HttpWebRequest)WebRequest.Create(URL);

            if (userName!=null && userName.Length > 1)
            {
                NetworkCredential myCred = new NetworkCredential(userName, password);
                CredentialCache MyCrendentialCache = new CredentialCache();
                MyCrendentialCache.Add(URL, "Basic", myCred);
                HttpWRequest.Credentials = MyCrendentialCache;
            }
            else
            {
                HttpWRequest.Credentials = CredentialCache.DefaultCredentials;
            }
            
            HttpWRequest.UserAgent = "Storefront HTTP Client";
            HttpWRequest.KeepAlive = true;
            HttpWRequest.Headers.Set("Pragma", "no-cache");
            HttpWRequest.Timeout = 300000;
            HttpWRequest.Method = "GET";

            if (null != HttpWResponse)
            {
                HttpWResponse.Close();
                HttpWResponse = null;
            }

            HttpWResponse = (HttpWebResponse)HttpWRequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader sr = new StreamReader(HttpWResponse.GetResponseStream(),enc);
            response = sr.ReadToEnd();
            sr.Close();
            HttpWResponse.Close();              

            return response;
        }
    }
}
