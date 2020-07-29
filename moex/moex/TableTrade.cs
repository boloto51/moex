using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace moex
{
    public class TableTrade
    {
        public void FillingDB(List<Security> secList, string Url_Security, string Url_Security_Postfix)
        {


            foreach (var secItem in secList)
            {
                //var _secId = _context.Securities.Where(b => b.SecId == secId).Select(b => b.Id).FirstOrDefault();
                var secId = secItem.SecId;
                var _secId = secItem.Id;

                var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, 0, secId);
                var sLineTotal = new JsonCreator().Create(objReader);
                Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

                int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100)) + 1;

                for (int i = 0; i < count; i++)
                {
                    objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i, secId);
                    sLineTotal = new JsonCreator().Create(objReader);

                    obj = JsonSerializer.Deserialize<Root>(sLineTotal);

                    foreach (var entry in obj.history.data)
                    {
                        Console.WriteLine("BOARDID: {0}\tTRADEDATE: {1}\tSECID: {2}\tOPEN: {3}\t" +
                            "LOW: {4}\tHIGH: {5}\tWAPRICE: {6}\tCLOSE: {7}",
                            entry.ToArray()[0], entry.ToArray()[1], secId, 
                            entry.ToArray()[6], entry.ToArray()[7], entry.ToArray()[8], 
                            entry.ToArray()[10], entry.ToArray()[11]);

                        DataContext _context = new DataContext();

                        var isDate = _context.Trades.Select(a => a.TradeDate.ToString() == entry.ToArray()[1].ToString()) != null;
                        var isBoard = _context.Trades.Select(a => a.BOARDID.ToString() == entry.ToArray()[0].ToString()) != null;
                        var isSecId = _context.Trades.Select(a => a.SecId == _secId) != null;

                        if (isDate && isSecId && isBoard)
                        {
                            _context.Trades.Add(new Trade
                            {
                                BOARDID = entry.ToArray()[0].ToString(),
                                TradeDate = entry.ToArray()[1].ToString(),
                                SecId = _secId,
                                OPEN = Convert.ToDecimal(entry.ToArray()[6].ToString()),
                                LOW = Convert.ToDecimal(entry.ToArray()[7].ToString()),
                                HIGH = Convert.ToDecimal(entry.ToArray()[8].ToString()),
                                WAPRICE = Convert.ToDecimal(entry.ToArray()[10].ToString()),
                                CLOSE = Convert.ToDecimal(entry.ToArray()[11].ToString())
                            });
                            _context.SaveChanges();
                        }
                        //_context.SaveChanges();
                    }

                    //_context.SaveChanges();
                }

                //Console.ReadLine();
            }
        }
    }
}
