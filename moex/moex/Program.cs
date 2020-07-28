using System;
using System.Net;
using System.IO;
using moex.JSON_class;

using System.Text.Json;
using System.Text.Json.Serialization;

using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using moex.DbContext;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            string sURL;
            sURL = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);

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

            Root obj = JsonSerializer.Deserialize<Root>(sLineNew);

            using (var _context = new DataContext())
            {
                foreach (var row in obj.history.data)
                {
                    Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", row.ToArray()[3], row.ToArray()[2]);

                    _context.Securities.Add(new Security
                    {
                        SecId = row.ToArray()[3].ToString(),
                        ShortName = row.ToArray()[2].ToString()
                    });
                    _context.SaveChanges();
                }
            }

            Console.ReadLine();
        }
    }
}
