using moex.Services;
using System;

namespace moex
{
    public class TableSecurity
    {
        Uri uri;
        HttpService httpService;
        JsonCreator jsonCreator;
        DataBase dataBase;

        public TableSecurity(Uri _uri, HttpService _httpService, JsonCreator _jsonCreator, DataBase _dataBase)
        {
            uri = _uri;
            httpService = _httpService;
            jsonCreator = _jsonCreator;
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
                var root = jsonCreator.Deserialize(url_init, postfix_json, postfix_start, i);
                dataBase.ToSecurityTableAsync(root);
            }

            Console.ReadLine();
        }
    }
}
