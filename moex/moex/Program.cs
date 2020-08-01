using System;
using System.Linq;
using System.Threading.Tasks;
using moex.DbContext;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext _context = new DataContext();

            //Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            //string Url_Security = "http://iss.moex.com/iss/history/engines/stock/markets/shares/securities";
            string url_init = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            string postfix_start = "?start=";

            //new TableSecurity().FillingDB(_context, Url_Security, Url_Security_Postfix);
            //var secList = _context.Securities.Select(a => a).ToList();
            //FillingDataBase fillDB = new FillingDataBase();

            //TableSecurity tableSecurity = new TableSecurity();
            //TableTrade tableTrade = new TableTrade();

            Uri uri = new Uri();
            var url_postfix = uri.ConcatenateUrl(url_init, postfix_start);
            var streamReader = uri.GetStreamFromUrl(url_postfix);
            var countHundredsPages = uri.GetCountHundredsPages(streamReader);

            JsonCreator jsonCreator = new JsonCreator();

            for (int i=0; i <= countHundredsPages; i++)
            {
                //var root =  await Task.Run(() => uri.Deserialize(url_init, postfix_start, i));
                //await Task.Run(() => uri.GetParsingData(root));
                var root = jsonCreator.Deserialize(url_init, postfix_start, i);
                jsonCreator.WritingToSecurityTable(root);
            }


            //foreach (var secItem in secList)
            //{
            //    //securityTable.FillingDB(secItem, Url_Security, Url_Security_Postfix);
            //    //tradeTable.FillingDB(secItem, Url_Security, Url_Security_Postfix);
            //    fillDB.FillingDB(secItem, Url_Security, Url_Security_Postfix, "securityTable");
            //}

            Console.ReadLine();
        }
    }
}
