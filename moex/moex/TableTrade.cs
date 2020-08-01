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
        ////public async void FillingDBAsync(Security secItem, string Url_Security, string Url_Security_Postfix)
        ////{
        ////    await Task.Run(() => FillingDB(secItem, Url_Security, Url_Security_Postfix));
        ////}

        //public async void FillingDB(Security secItem, string Url_Security, string Url_Security_Postfix)
        //{
        //    var secId = secItem.SecId;
        //    var _secId = secItem.Id;

        //    var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, 0, secId);
        //    var sLineTotal = new JsonCreator().Create(objReader);
        //    Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

        //    int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100));

        //    for (int i = 0; i <= count; i++)
        //    {
        //        await Task.Run(() => ParsingPage(Url_Security, Url_Security_Postfix, i, secId, _secId));
        //    }
        //}

        //public void ParsingPage(string Url_Security, string Url_Security_Postfix, int i, string secId, int _secId)
        //{
        //    var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i, secId);
        //    var sLineTotal = new JsonCreator().Create(objReader);

        //    var obj = JsonSerializer.Deserialize<Root>(sLineTotal);

        //    var url = new StreamReaderFromUrl().ConcatenateUrl(Url_Security, Url_Security_Postfix, i, secId);

        //    foreach (var entry in obj.history.data)
        //    {
        //        Console.WriteLine("TRADEDATE: {0}\tSECIDstr: {1}\tCLOSE: {2}",
        //            entry.ToArray()[1], entry.ToArray()[3],entry.ToArray()[11]);

        //        DataContext _context = new DataContext();

        //        _context.Trades.Add(new Trade
        //        {
        //            TradeDate = DateTime.Parse(entry.ToList()[1].ToString()),
        //            SecId = _secId,
        //            //CLOSE = entry.ToList()[11] == null ? null : Convert.ToDecimal(entry.ToList()[11].ToString())
        //        });
        //        _context.SaveChanges();
        //    }
        //}
    }
}
