using moex.JSON_class;
using System;
using System.Collections.Generic;
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
            //string postfix_date_end = "2020-08-01";

            var secList = dataBaseGet.FromSecurityTable();

            foreach(var secItem in secList)
            {
                await Task.Run(() => StartFromFirstPage(url_init, secItem.SecId, postfix_json, postfix_from, postfix_date_init));
            }
        }

        public async void StartFromFirstPage(string url_init, string secId, string postfix_json, string postfix_from, string postfix_date_init)
        {
            var date = postfix_date_init;

            while (DateTime.Parse(date) <= DateTime.Now)
            {
                Uri uri = new Uri();
                var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
                var streamReader = uri.GetStreamFromUrl(url);
                var pageLastData = uri.GetPageLastData(streamReader);
                await Task.Run(() => Fill(url_init, secId, postfix_json, postfix_from, date));
                date = ConvertDate(pageLastData.AddDays(1));
            }
        }

        public void Fill(string url_init, string secId, string postfix_json, string postfix_from, string date)
        {
            JsonCreator jsonCreator = new JsonCreator();
            DataBasePost dataBasePost = new DataBasePost();
            var root = jsonCreator.Deserialize(url_init, secId, postfix_json, postfix_from, date);
            dataBasePost.ToTradeTableAsync(root);
        }

        public string ConvertDate(DateTime date)
        {
            return date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString();
        }
    }
}
