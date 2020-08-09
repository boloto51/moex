using System;
using moex.DbContext;
using moex.Services;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            string url_init = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities";
            string postfix_date_init = "2015-01-01";
            Uri uri = new Uri();
            HttpService httpService = new HttpService();
            JsonCreator jsonCreator = new JsonCreator(uri, httpService);
            DataBase dataBase = new DataBase();

            //TableSecurity tableSecurity = new TableSecurity();
            //tableSecurity.Fill(httpService, uri, url_init);

            TableTrade tableTrade = new TableTrade(uri, httpService, jsonCreator, dataBase);

            if (tableTrade.CheckData() == false)
            {
                tableTrade.FillAsync(url_init, postfix_date_init);
            }
            else
            {
                tableTrade.UpdateTable(url_init);
            }
        }
    }
}
