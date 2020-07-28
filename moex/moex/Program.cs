using System;
using System.Net;
using System.IO;
using moex.JSON_class;

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

            Root obj = JsonConvert.DeserializeObject<Root>(sLineNew);

            foreach (var row in obj.history.data)
            {
                Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", row.ToArray()[3], row.ToArray()[2]);
            }

            //Console.WriteLine(obj.history.data.ToString());
            Console.ReadLine();
        }
    }
}
