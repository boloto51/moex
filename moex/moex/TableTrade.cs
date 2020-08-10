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
        JsonCreator jsonCreator;
        DataBase dataBase;

        public TableTrade(Uri _uri, HttpService _httpService, JsonCreator _jsonCreator, DataBase _dataBase)
        {
            uri = _uri;
            httpService = _httpService;
            jsonCreator = _jsonCreator;
            dataBase = _dataBase;
        }

        public async void FillAsync(string url_init, string postfix_date_init)
        {
            //string postfix_json = ".json";
            //string postfix_from = "?from=";

            var secList = dataBase.FromSecurityTable();

            foreach (var secItem in secList)
            {
                //StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_json, postfix_from, postfix_date_init);
                //await Task.Run(() => StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_json, postfix_from, postfix_date_init));
                StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_date_init);
                //await Task.Run(() => StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_date_init));
            }
        }

        //public void FillAsync(string secId, string url_init, string postfix_date_last)
        //{
        //    //string postfix_json = ".json";
        //    //string postfix_from = "?from=";

        //    //StartFromSpecifiedPage(uri, url_init, secId, postfix_json, postfix_from, postfix_date_last);
        //    StartFromSpecifiedPage(uri, url_init, secId, postfix_date_last);
        //}

        public void StartFromSpecifiedPage(Uri uri, string url_init, string secId, string postfix_date_init)
        {
            string postfix_json = ".json";
            string postfix_from = "?from=";

            var date = postfix_date_init;
            //var dateEnd = "2015-07-31";
            var dateEnd = DateTime.Now.AddDays(-1).ToString();

            while (DateTime.Compare(DateTime.Parse(date), DateTime.Parse(dateEnd)) <= 0)
            {
                var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
                Root root = httpService.GetAsync1<Root>(url).Result;
                int count = uri.GetPageLastDataCount(root);

                if (count != 0)
                {
                    var pageLastData = uri.GetPageLastData(root, count);
                    //Fill(httpService, uri, url_init, secId, postfix_json, postfix_from, date);
                    //dataBase.ToTradeTableAsync(root, url_init, secId, postfix_json, postfix_from, date);
                    //await Task.Run(() => dataBase.ToTradeTable(root, url_init, secId, postfix_json, postfix_from, date));
                    dataBase.ToTradeTable(root, url_init, secId, postfix_json, postfix_from, date);
                    date = ConvertDate(pageLastData.AddDays(1));
                }
            }
        }

        //public async void Fill(HttpService httpService, Uri uri, string url_init, string secId, string postfix_json, string postfix_from, string date)
        //{
        //    var url_param = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
        //    //var root = jsonCreator.Deserialize(url_init, secId, postfix_json, postfix_from, date);
        //    var root = await Task.Run(() => httpService.GetAsync1<Root>(url_param));
        //    dataBase.ToTradeTableAsync(root, url_init, secId, postfix_json, postfix_from, date);
        //}

        public string ConvertDate(DateTime date)
        {
            var month = date.Month.ToString().Length < 2 ? "0" + date.Month.ToString() : date.Month.ToString();
            var day = date.Day.ToString().Length < 2 ? "0" + date.Day.ToString() : date.Day.ToString();
            return date.Year.ToString() + "-" + month + "-" + day;
        }

        public void UpdateTable(string url_init)
        {
            //DataBase dataBaseAsync = new DataBase();

            //var secList = dataBase.FromSecurityTable();
            //List<Trade> lastTradesInDB = new List<Trade>();

            //foreach (var secItem in secList)
            //{
            //    //List<Trade> lastTradeInDB =  await Task.Run(() => SearchLastDateInDB(url_init, secItem.SECID, postfix_json, postfix_from));
            //    //lastTradeInDB.Add(dataBase.FindLastDate(secItem.SECID));
            //    //var trade = await Task.Run(() => FindLastDateInDB(secItem.SECID, dataBase));
            //    //var trade = await Task.Run(() => dataBase.FindLastDate(secItem.SECID));
            //    var trade = await Task.Run(() => dataBase.FindLastDate(secItem.SECID));
            //    lastTradesInDB.Add(trade);
            //}

            var lastTradesInDB = dataBase.FindLastTrades();

            foreach (var lastTrade in lastTradesInDB)
            {
                string postfix_date_last = ConvertDate(lastTrade.TRADEDATE.Date.AddDays(1));
                //FillAsync(lastTrade.SECID, url_init, postfix_date_last);
                StartFromSpecifiedPage(uri, url_init, lastTrade.SECID, postfix_date_last);
                //await Task.Run(() => FillAsync(lastTrade.SECID, url_init, postfix_date_last));
                //await Task.Run(() => FillAsync(lastTrade.SECID, url_init, postfix_date_last));
                //await Task.Run(() => StartFromSpecifiedPage(uri, url_init, lastTrade.SECID, postfix_date_last));
            }
        }

        //public Trade FindLastDateInDB(string secId, DataBase dataBase)
        //{
        //    //return await Task.Run(() => dataBase.FindLastDate(secId));
        //    return dataBase.FindLastDate(secId);
        //}
    }
}
