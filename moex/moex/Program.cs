using System;
using moex.Services;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            string url_init = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities";
            //string postfix_date_init = "2015-01-01";
            string postfix_date_init = new TableTrade().ConvertDate(DateTime.Now.AddYears(-5));

            HttpService httpService = new HttpService();
            Uri uri = new Uri(httpService);
            DataBase dataBase = new DataBase();

            TableSecurity tableSecurity = new TableSecurity(dataBase);

            if (dataBase.FromSecurityTableCount() == 0)
            {
                tableSecurity.Fill(httpService, uri, url_init);
            }

            TableTrade tableTrade = new TableTrade(uri, httpService, dataBase);

            if (dataBase.FromTradeTableCount() == 0)
            {
                tableTrade.Fill(url_init, postfix_date_init);
            }
            else
            {
                tableTrade.UpdateTable(url_init);
            }
        }
    }
}
