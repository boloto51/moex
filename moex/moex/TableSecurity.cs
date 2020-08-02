using moex.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace moex
{
    public class TableSecurity
    {
        public void Fill(string url_init)
        {
            Uri uri = new Uri();
            JsonCreator jsonCreator = new JsonCreator();
            DataBase dataBase = new DataBase();

            string postfix_json = ".json";
            string postfix_start = "?start=";

            var url_postfix = uri.ConcatenateUrl(url_init, postfix_json, postfix_start);
            var streamReader = uri.GetStreamFromUrl(url_postfix);
            var countHundredsPages = uri.GetCountHundredsPages(streamReader);

            for (int i = 0; i <= countHundredsPages; i++)
            {
                var root = jsonCreator.Deserialize(url_init, postfix_json, postfix_start, i);
                dataBase.WritingToSecurityTable(root);
            }

            Console.ReadLine();
        }
    }
}
