using System;
using System.Linq;
using moex.DbContext;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext _context = new DataContext();

            //Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            string Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/securities";
            string Url_Security_Postfix = "?start=";

            //new TableSecurity().FillingDB(_context, Url_Security, Url_Security_Postfix);

            var secList = _context.Securities.Select(a => a).ToList();

            TableTrade tradeTable = new TableTrade();

            foreach (var secItem in secList)
            {
                tradeTable.FillingDBAsync(secItem, Url_Security, Url_Security_Postfix);
            }

            Console.ReadLine();
        }
    }
}
