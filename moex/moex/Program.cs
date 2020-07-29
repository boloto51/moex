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
using moex.Models;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext _context = new DataContext();

            //Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            string Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/securities.json";
            string Url_Security_Postfix = "?start=";

            for (int i = 0; i < 5; i++)
            {
                var objReader = new StreamReaderFromUrl().Read(i, Url_Security, Url_Security_Postfix);
                var sLineTotal = new JsonCreator().Create(objReader);

                Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

                foreach (var row in obj.history.data)
                {
                    Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", row.ToArray()[3], row.ToArray()[2]);

                    if (_context.Securities.Select(a => a.SecId == row.ToArray()[3].ToString()) != null)
                    {
                        _context.Securities.Add(new Security
                        {
                            SecId = row.ToArray()[3].ToString(),
                            ShortName = row.ToArray()[2].ToString()
                        });
                        _context.SaveChanges();
                    }                    
                }
            }

            Console.ReadLine();
        }
    }
}
