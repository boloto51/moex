using System;
using System.Net;
using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://support.microsoft.com/ru-ru/help/307023/how-to-make-a-get-request-by-using-visual-c
            string sURL;
            sURL = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

            //string sLine = "";
            //int i = 0;

            //while (sLine != null)
            //{
            //    i++;
            //    sLine = objReader.ReadLine();
            //    if (sLine != null)
            //        Console.WriteLine("{0}:{1}", i, sLine);
            //}
            //Console.ReadLine();

            string sLine = "";
            string sLineNew = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    sLineNew = sLineNew + sLine.Trim();
            }

            JObject obj = JObject.Parse(sLineNew);
            var metadata = obj["history.cursor"].ToString();

            Console.WriteLine(metadata);
            Console.ReadLine();
        }
    }
}
