using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace moex
{
    public class TableSecurity
    {
        public void FillingDB(DataContext _context, string Url_Security, string Url_Security_Postfix, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i);
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
