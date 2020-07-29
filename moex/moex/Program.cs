﻿using System;
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
            //DataContext _context = new DataContext();

            //Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            string Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/securities";
            string Url_Security_Postfix = "?start=";

            //new TableSecurity().FillingDB(_context, Url_Security, Url_Security_Postfix);

            DataContext _context = new DataContext();

            var secList = _context.Securities.Select(a => a).ToList();

            new TableTrade().FillingDB(secList, Url_Security, Url_Security_Postfix);

            Console.ReadLine();
        }
    }
}
