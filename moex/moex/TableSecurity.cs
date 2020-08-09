using moex.Services;
using System;

namespace moex
{
    public class TableSecurity
    {
        public void Fill(HttpService httpService, Uri uri, string url_init)
        {
            //Uri uri = new Uri();
            JsonCreator jsonCreator = new JsonCreator(uri, httpService);
            DataBase dataBasePost = new DataBase();

            string postfix_json = ".json";
            string postfix_start = "?start=";

            var url_postfix = uri.ConcatenateUrlStart(url_init, postfix_json, postfix_start);
            //var streamReader = uri.GetStreamFromUrl(url_postfix);
            var countHundredsPages = uri.GetCountHundredsPages(url_postfix);

            for (int i = 0; i <= countHundredsPages; i++)
            {
                var root = jsonCreator.Deserialize(url_init, postfix_json, postfix_start, i);
                dataBasePost.ToSecurityTableAsync(root);
            }

            Console.ReadLine();
        }
    }
}
