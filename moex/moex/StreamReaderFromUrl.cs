using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace moex
{
    public class StreamReaderFromUrl
    {
        public StreamReader Read(string Url_Security, string Url_Security_Postfix, int i = 0, string SecId = "")
        {
            var url = SecId == ""
                ? Url_Security + Url_Security_Postfix + Convert.ToString(i * 100)
                : Url_Security + "/" + SecId + ".json" + Url_Security_Postfix + Convert.ToString(i * 100);
            WebRequest wrGETURL = WebRequest.Create(url);
            Stream objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            return objReader;
        }
    }
}
