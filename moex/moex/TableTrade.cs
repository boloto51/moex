using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    public class TableTrade
    {
        public async void FillingDBAsync(Security secItem, string Url_Security, string Url_Security_Postfix)
        {
            await Task.Run(() => FillingDB(secItem, Url_Security, Url_Security_Postfix));
        }

        async void FillingDB(Security secItem, string Url_Security, string Url_Security_Postfix)
        {
            //foreach (var secItem in secList)
            //for (int k = 256; k <= secList.ToArray().Count(); k++)
            {
                var secId = secItem.SecId;
                var _secId = secItem.Id;
                //var secId = secList[k].SecId;
                //var _secId = secList[k].Id;

                var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, 0, secId);
                var sLineTotal = new JsonCreator().Create(objReader);
                Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

                int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100));

                for (int i = 0; i <= count; i++)
                {
                    await Task.Run(() => ParsingPage(Url_Security, Url_Security_Postfix, i, secId, _secId));

                    //objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i, secId);
                    //sLineTotal = new JsonCreator().Create(objReader);

                    //obj = JsonSerializer.Deserialize<Root>(sLineTotal);

                    //foreach (var entry in obj.history.data)
                    //{
                    //    Console.WriteLine("BOARDID: {0}\tTRADEDATE: {1}\tSECIDstr: {2}\tSECID: {3}\tOPEN: {4}\t" +
                    //        "LOW: {5}\tHIGH: {6}\tWAPRICE: {7}\tCLOSE: {8}\tURL: {9}",
                    //        entry.ToArray()[0], entry.ToArray()[1], entry.ToArray()[3], secId, 
                    //        entry.ToArray()[6], entry.ToArray()[7], entry.ToArray()[8], 
                    //        entry.ToArray()[10], entry.ToArray()[11], 
                    //        new StreamReaderFromUrl().CreateUrl(Url_Security, Url_Security_Postfix, i, secId));

                    //    DataContext _context = new DataContext();

                    //    //var isBoard = _context.Trades.Select(a => a.BOARDID.ToString() == entry.ToArray()[0].ToString());
                    //    //var isDate = _context.Trades.Select(a => a.TradeDate.ToString() == entry.ToArray()[1].ToString());
                    //    //var isSecId = _context.Trades.Select(a => a.SecId == _secId);

                    //    //if ((isDate == null) && (isSecId == null) && (isBoard == null))
                    //    //if (!String.IsNullOrEmpty(entry.ToString()))
                    //    //{
                    //        _context.Trades.Add(new Trade
                    //        {
                    //            BOARDID = String.IsNullOrEmpty(entry.ToList()[0].ToString()) ? null : entry.ToList()[0].ToString(),
                    //            TradeDate = String.IsNullOrEmpty(entry.ToList()[1].ToString()) ? null : entry.ToList()[1].ToString(),
                    //            SHORTNAME = String.IsNullOrEmpty(entry.ToList()[2].ToString()) ? null : entry.ToList()[2].ToString(),
                    //            SECIDstr = String.IsNullOrEmpty(entry.ToList()[3].ToString()) ? null : entry.ToList()[3].ToString(),
                    //            SecId = _secId,
                    //            OPEN = entry.ToList()[6] == null ? null : entry.ToList()[6].ToString(),
                    //            LOW = entry.ToList()[7] == null ? null : entry.ToList()[7].ToString(),
                    //            HIGH = entry.ToList()[8] == null ? null : entry.ToList()[8].ToString(),
                    //            WAPRICE = entry.ToList()[10] == null ? null : entry.ToList()[10].ToString(),
                    //            CLOSE = entry.ToList()[11] == null ? null : entry.ToList()[11].ToString()
                    //        });
                    //        _context.SaveChanges();
                    //    //}
                    //}
                }
                //}
            }
        }

        void ParsingPage(string Url_Security, string Url_Security_Postfix, int i, string secId, int _secId)
        {
            var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i, secId);
            var sLineTotal = new JsonCreator().Create(objReader);

            var obj = JsonSerializer.Deserialize<Root>(sLineTotal);

            var url = new StreamReaderFromUrl().CreateUrl(Url_Security, Url_Security_Postfix, i, secId);

            foreach (var entry in obj.history.data)
            {
                Console.WriteLine("BOARDID: {0}\tTRADEDATE: {1}\tSECIDstr: {2}\tSECID: {3}\tOPEN: {4}\t" +
                    "LOW: {5}\tHIGH: {6}\tWAPRICE: {7}\tCLOSE: {8}\tURL: {9}",
                    entry.ToArray()[0], entry.ToArray()[1], entry.ToArray()[3], secId,
                    entry.ToArray()[6], entry.ToArray()[7], entry.ToArray()[8],
                    entry.ToArray()[10], entry.ToArray()[11], url);

                DataContext _context = new DataContext();

                //var isBoard = _context.Trades.Select(a => a.BOARDID.ToString() == entry.ToArray()[0].ToString());
                //var isDate = _context.Trades.Select(a => a.TradeDate.ToString() == entry.ToArray()[1].ToString());
                //var isSecId = _context.Trades.Select(a => a.SecId == _secId);

                //if ((isDate == null) && (isSecId == null) && (isBoard == null))
                //if (!String.IsNullOrEmpty(entry.ToString()))
                //{
                _context.Trades.Add(new Trade
                {
                    BOARDID = String.IsNullOrEmpty(entry.ToList()[0].ToString()) ? null : entry.ToList()[0].ToString(),
                    TradeDate = String.IsNullOrEmpty(entry.ToList()[1].ToString()) ? null : entry.ToList()[1].ToString(),
                    SHORTNAME = String.IsNullOrEmpty(entry.ToList()[2].ToString()) ? null : entry.ToList()[2].ToString(),
                    SECIDstr = String.IsNullOrEmpty(entry.ToList()[3].ToString()) ? null : entry.ToList()[3].ToString(),
                    SecId = _secId,
                    OPEN = entry.ToList()[6] == null ? null : entry.ToList()[6].ToString(),
                    LOW = entry.ToList()[7] == null ? null : entry.ToList()[7].ToString(),
                    HIGH = entry.ToList()[8] == null ? null : entry.ToList()[8].ToString(),
                    WAPRICE = entry.ToList()[10] == null ? null : entry.ToList()[10].ToString(),
                    CLOSE = entry.ToList()[11] == null ? null : entry.ToList()[11].ToString(),
                    URL = url
                });
                _context.SaveChanges();
                //}
            }
        }
    }
}
