using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace TDJ.Panel.Library
{
    public class FirebaseClient : INotifier
    {
        private const string uri = "https://tdj-notification-panel.firebaseio.com/Panel.json?auth=VTWCbE7djeQMVw0017ui2UZWoUT0jnDNDC9I1VPP";

        public JObject SendMessage<T>(T obj)
        {
            dynamic result;

            var json = JsonConvert.SerializeObject(obj);
            var text = "{\"PanelMessage\":" + json + "}";
            var req = HttpWebRequest.Create(uri);
            req.Method = "PATCH";
            req.ContentType = "application/json";
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(text);

            req.ContentLength = bytes.Length;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var resp = req.GetResponse())
            {
                var results = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                result = JObject.Parse(results);
            }
            return result;
        }
    }
}
