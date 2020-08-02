using moex.JSON_class;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    class TableTrade
    {
        public async void FillAsync(string url_init)
        {
            DataBaseGet dataBaseGet = new DataBaseGet();

            string postfix_json = ".json";
            string postfix_from = "?from=";
            string postfix_date_init = "2015-01-01";

            var secList = dataBaseGet.FromSecurityTable();

            foreach(var secItem in secList)
            {
                await Task.Run(() => Fill(url_init, secItem.SecId, postfix_json, postfix_from, postfix_date_init));
            }
        }

        public void Fill(string url_init, string secId, string postfix_json, string postfix_from, string postfix_date_init)
        {
            Uri uri = new Uri();
            JsonCreator jsonCreator = new JsonCreator();
            var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, postfix_date_init);
            var streamReader = uri.GetStreamFromUrl(url);
            var pageLastData = uri.GetPageLastData(streamReader);

        }
    }
}
