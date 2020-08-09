using moex.JSON_class;
using moex.Models;
using moex.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
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

        public void FillAsync(string url_init, string postfix_date_init)
        {
            DataBase dataBase = new DataBase();

            string postfix_json = ".json";
            string postfix_from = "?from=";
            //string postfix_date_init = "2015-01-01";

            var secList = dataBase.FromSecurityTable();

            foreach(var secItem in secList)
            {
                StartFromSpecifiedPage(uri, url_init, secItem.SECID, postfix_json, postfix_from, postfix_date_init);
            }
        }

        public void FillAsync(string secId, string url_init, string postfix_date_last)
        {
            string postfix_json = ".json";
            string postfix_from = "?from=";

            StartFromSpecifiedPage(uri, url_init, secId, postfix_json, postfix_from, postfix_date_last);
        }

        public async void StartFromSpecifiedPage(Uri uri, string url_init, string secId, string postfix_json, string postfix_from, string postfix_date_init)
        {
            var date = postfix_date_init;
            //var dateEnd = "2015-07-31";
            var dateEnd = DateTime.Now.ToString();

            while (DateTime.Compare(DateTime.Parse(date), DateTime.Parse(dateEnd).AddDays(-1)) <= 0)
            {
                //Uri uri = new Uri();
                var url = uri.ConcatenateUrlFrom(url_init, secId, postfix_json, postfix_from, date);
                //var streamReader = uri.GetStreamFromUrl(url);
                //var pageLastData = uri.GetPageLastData(streamReader);
                //Root root = uri.GetPageLastDataRoot(streamReader);
                Root root = Task.Run(() => httpService.GetAsync1<Root>(url)).Result;
                int count = uri.GetPageLastDataCount(root);
                if (count != 0)
                {
                    var pageLastData = uri.GetPageLastData(root, count);
                    await Task.Run(() => Fill(httpService, uri, url_init, secId, postfix_json, postfix_from, date));
                    date = ConvertDate(pageLastData.AddDays(1));
                }
            }
        }

        public void Fill(HttpService httpService, Uri uri, string url_init, string secId, string postfix_json, string postfix_from, string date)
        {
            var root = jsonCreator.Deserialize(url_init, secId, postfix_json, postfix_from, date);
            dataBase.ToTradeTableAsync(root, url_init, secId, postfix_json, postfix_from, date);
        }

        public string ConvertDate(DateTime date)
        {
            var month = date.Month.ToString().Length < 2 ? "0" + date.Month.ToString() : date.Month.ToString();
            var day = date.Day.ToString().Length < 2 ? "0" + date.Day.ToString() : date.Day.ToString();
            return date.Year.ToString() + "-" + month + "-" + day;
        }

        public bool CheckData()
        {
            DataBase dataBase = new DataBase();
            var secList = dataBase.FromSecurityTable();
            return secList.Count != 0 ? true : false;
        }

        public async void UpdateTable(string url_init)
        {
            //DataBase dataBaseAsync = new DataBase();

            var secList = dataBase.FromSecurityTable();
            List<Trade> lastTradesInDB = new List<Trade>();

            foreach (var secItem in secList)
            {
                DataBase dataBaseAsync = new DataBase();
                //List<Trade> lastTradeInDB =  await Task.Run(() => SearchLastDateInDB(url_init, secItem.SECID, postfix_json, postfix_from));
                //lastTradeInDB.Add(dataBase.FindLastDate(secItem.SECID));
                //var trade = await Task.Run(() => FindLastDateInDB(secItem.SECID, dataBase));
                var trade = await Task.Run(() => dataBaseAsync.FindLastDate(secItem.SECID));
                //var trade = dataBaseAsync.FindLastDate(secItem.SECID);
                lastTradesInDB.Add(trade);
            }

            foreach (var lastTrade in lastTradesInDB)
            {
                string postfix_date_last = ConvertDate(lastTrade.TRADEDATE.Date.AddDays(1));
                FillAsync(lastTrade.SECID, url_init, postfix_date_last);
            }
        }

        //public Trade FindLastDateInDB(string secId, DataBase dataBase)
        //{
        //    //return await Task.Run(() => dataBase.FindLastDate(secId));
        //    return dataBase.FindLastDate(secId);
        //}
    }
}
