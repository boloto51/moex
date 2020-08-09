using moex.JSON_class;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    class TableTrade
    {
        public void FillAsync(string url_init)
        {
            DataBaseGet dataBaseGet = new DataBaseGet();

            string postfix_json = ".json";
            string postfix_from = "?from=";
            string postfix_date_init = "2015-01-01";

            var secList = dataBaseGet.FromSecurityTable();

            foreach(var secItem in secList)
            {
                StartFromFirstPage(url_init, secItem.SECID, postfix_json, postfix_from, postfix_date_init);
            }
        }

        public async void StartFromFirstPage(string url_init, string secId, string postfix_json, string postfix_from, string postfix_date_init)
        {
            var date = postfix_date_init;
            //var dateEnd = "2015-07-31";
            var dateEnd = DateTime.Now.ToString();

            while (DateTime.Compare(DateTime.Parse(date), DateTime.Parse(dateEnd).AddDays(-1)) <= 0)
            {
                Uri uri = new Uri();
                var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
                var streamReader = uri.GetStreamFromUrl(url);
                //var pageLastData = uri.GetPageLastData(streamReader);
                Root root = uri.GetPageLastDataRoot(streamReader);
                int count = uri.GetPageLastDataCount(root);
                if (count != 0)
                {
                    var pageLastData = uri.GetPageLastData(root, count);
                    await Task.Run(() => Fill(url_init, secId, postfix_json, postfix_from, date));
                    date = ConvertDate(pageLastData.AddDays(1));
                }
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
            var month = date.Month.ToString().Length < 2 ? "0" + date.Month.ToString() : date.Month.ToString();
            var day = date.Day.ToString().Length < 2 ? "0" + date.Day.ToString() : date.Day.ToString();
            return date.Year.ToString() + "-" + month + "-" + day;
        }
    }
}
