using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace StorefrontModel
{
    public class CarrierRequester
    {
        protected object Request(string url, object auth, object request, Type responseType)
        {
            object response = null;

            try
            {
                byte[] access = auth!=null?SerializeObject(auth):null;
                byte[] data = SerializeObject(request);

                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                wr.Method = "POST";
                wr.KeepAlive = false;
                wr.UserAgent = "Storfront";
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.ContentLength = data.Length;
                if (access != null)
                {
                    wr.ContentLength += access.Length;
                }

                WebProxy proxyObject = new WebProxy("nproxyw.is.bear.com:8080", true);
                proxyObject.Credentials = new NetworkCredential("agraffeo", "bones08");
                GlobalProxySelection.Select = proxyObject;

                Stream SendStream = wr.GetRequestStream();
                if (access != null)
                {
                    SendStream.Write(access, 0, access.Length);
                }
                SendStream.Write(data, 0, data.Length);
                SendStream.Close();

                string result = null;
                HttpWebResponse WebResp = (HttpWebResponse)wr.GetResponse();
                using (StreamReader sr = new StreamReader(WebResp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                WebResp.Close();

                XmlSerializer xs = new XmlSerializer(responseType);

                try
                {
                    response = xs.Deserialize(new StringReader(result));
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                response = null;
            }

            return response;
        }

        private byte[] SerializeObject(object obj)
        {
            try
            {
                byte[] data = null;
                using (StringWriter writer = new StringWriter())
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                    xs.Serialize(writer, obj);
                    data = Encoding.ASCII.GetBytes(writer.ToString());
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private object DeserializeObject(Type t, string data)
        {
            XmlSerializer xs = new XmlSerializer(t);
            object response = null;
            try
            {
                response = xs.Deserialize(new StringReader(data));
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return response;
        } 
    }
}
