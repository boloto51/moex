using moex.JSON_class;
using moex.Services;
using System;
using System.Threading.Tasks;

namespace moex
{
    class TableTrade
    {
        Uri uri;
        HttpService httpService;
        DataBase dataBase;

        public TableTrade() { }

        public TableTrade(Uri _uri, HttpService _httpService, DataBase _dataBase)
        {
            uri = _uri;
            httpService = _httpService;
            dataBase = _dataBase;
        }

        public void Fill(string url_init, string postfix_date_init)
        {
            var secList = dataBase.FromSecurityTable();

            foreach (var secItem in secList)
            {
                StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_date_init);
            }
        }

        public async void StartFromSpecifiedPage(Uri uri, string url_init, string secId, string postfix_date_init)
        {
            string postfix_json = ".json";
            string postfix_from = "?from=";

            var date = postfix_date_init;
            var dateEnd = ConvertDate(DateTime.Now.Date.AddDays(-1)).ToString();

            while (DateTime.Compare(DateTime.Parse(date), DateTime.Parse(dateEnd)) <= 0)
            {
                var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
                Root root = httpService.GetAsync1<Root>(url).Result;

                if (root != null)
                {
                    int count = uri.GetPageLastDataCount(root);

                    if (count != 0)
                    {
                        var pageLastData = uri.GetPageLastData(root, count);
                        await Task.Run(() => dataBase.ToTradeTable(root, url_init, secId, postfix_json, postfix_from, date));
                        date = ConvertDate(pageLastData.Date.AddDays(1));
                    }
                    else
                    {
                        date = ConvertDate(DateTime.Parse(date).Date.AddDays(1));
                    }
                }
                else
                {
                    date = ConvertDate(DateTime.Parse(date).Date.AddDays(1));
                }
            }
        }

        public string ConvertDate(DateTime date)
        {
            var month = date.Month.ToString().Length < 2 ? "0" + date.Month.ToString() : date.Month.ToString();
            var day = date.Day.ToString().Length < 2 ? "0" + date.Day.ToString() : date.Day.ToString();
            return date.Year.ToString() + "-" + month + "-" + day;
        }

        public void UpdateTable(string url_init)
        {
            var lastTradesInDB = dataBase.FindLastTrades();

            foreach (var lastTrade in lastTradesInDB)
            {
                string postfix_date_last = ConvertDate(lastTrade.TRADEDATE.Date.AddDays(1));
                StartFromSpecifiedPage(uri, url_init, lastTrade.SECID, postfix_date_last);
            }
        }
    }
}
