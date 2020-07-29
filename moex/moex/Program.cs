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

            var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix);
            var sLineTotal = new JsonCreator().Create(objReader);
            Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);
            int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100)) + 1;
            new TableSecurity().FillingDB(_context, Url_Security, Url_Security_Postfix, count);
        }
    }
}
