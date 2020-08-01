using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace moex
{
    public class StreamReaderFromUrl
    {
        
        public StreamReader Read(string Url_Security, string Url_Security_Postfix, int i = 0, string SecId = "")
        {
            //var url = SecId == ""
            //    ? Url_Security + Url_Security_Postfix + Convert.ToString(i * 100)
            //    : Url_Security + "/" + SecId + ".json" + Url_Security_Postfix + Convert.ToString(i * 100);
            //var url = ConcatenateUrl(Url_Security, Url_Security_Postfix, i, SecId);
            //WebRequest wrGETURL = WebRequest.Create(url);
            //wrGETURL.Timeout = 100000;
            //Stream objStream = wrGETURL.GetResponse().GetResponseStream();
            //StreamReader objReader = new StreamReader(objStream);
            //return objReader;

            var url = ConcatenateUrl(Url_Security, Url_Security_Postfix, i, SecId);
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            var httpGet = httpClient.GetAsync(url);
            var stream = httpGet.Result.Content.ReadAsStreamAsync();


            StreamReader objReader = new StreamReader(stream.Result);
            return objReader;
        }

        public string ConcatenateUrl(string Url_Security, string Url_Security_Postfix, int i = 0, string SecId = "")
        {
            var url = SecId == ""
                ? Url_Security + Url_Security_Postfix + Convert.ToString(i * 100)
                : Url_Security + "/" + SecId + ".json" + Url_Security_Postfix + Convert.ToString(i * 100);
            return url;
        }
    }
}
