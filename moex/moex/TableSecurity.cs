using moex.JSON_class;
using moex.Services;
using System;

namespace moex
{
    public class TableSecurity
    {
        DataBase dataBase;

        public TableSecurity(DataBase _dataBase)
        {
            dataBase = _dataBase;
        }

        public void Fill(HttpService httpService, Uri uri, string url_init)
        {
            string postfix_json = ".json";
            string postfix_start = "?start=";

            var url_postfix = uri.ConcatenateUrlStart(url_init, postfix_json, postfix_start);
            var countHundredsPages = uri.GetCountHundredsPages(url_postfix);

            for (int i = 0; i <= countHundredsPages; i++)
            {
                var url_param = uri.ConcatenateUrlStart(url_init, postfix_json, postfix_start, i);
                var root = httpService.GetAsync1<Root>(url_param).Result;
                dataBase.ToSecurityTableAsync(root);
            }

            Console.ReadLine();
        }
    }
}
