using System;

namespace moex
{
    public class TableSecurity
    {
        public void Fill(string url_init)
        {
            Uri uri = new Uri();
            JsonCreator jsonCreator = new JsonCreator();
            DataBasePost dataBasePost = new DataBasePost();

            string postfix_json = ".json";
            string postfix_start = "?start=";

            var url_postfix = uri.ConcatenateUrlStart(url_init, postfix_json, postfix_start);
            var streamReader = uri.GetStreamFromUrl(url_postfix);
            var countHundredsPages = uri.GetCountHundredsPages(streamReader);

            for (int i = 0; i <= countHundredsPages; i++)
            {
                var root = jsonCreator.Deserialize(url_init, postfix_json, postfix_start, i);
                dataBasePost.ToSecurityTableAsync(root);
            }

            Console.ReadLine();
        }
    }
}
