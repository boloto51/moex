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
        public void FillingDB(DataContext _context, string Url_Security, string Url_Security_Postfix)
        {
            foreach (var secId in _context.Securities.Select(a => a.SecId))
            {
                var _secId = _context.Securities.Where(b => b.SecId == secId).Select(b => b.Id).FirstOrDefault();

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
                            "LOW: {4}\tHIGH: {5}\tLEGALCLOSEPRICE: {6}\tWAPRICE: {7}\tCLOSE: {8}",
                            entry.ToArray()[0], entry.ToArray()[1], secId, 
                            entry.ToArray()[6], entry.ToArray()[7], entry.ToArray()[8], 
                            entry.ToArray()[9], entry.ToArray()[10], entry.ToArray()[11]);

                        if ((_context.Trades.Select(a => a.TradeDate.ToString() == entry.ToArray()[1].ToString()) != null) 
                            && 
                            (_context.Trades.Select(a => a.SecId == _secId) != null)
                            &&
                            (_context.Trades.Select(a => a.BOARDID.ToString() == entry.ToArray()[0].ToString()) != null))
                        {
                            _context.Trades.Add(new Trade
                            {
                                BOARDID = entry.ToArray()[0].ToString(),
                                TradeDate = entry.ToArray()[1].ToString(),
                                SecId = _secId,
                                OPEN = Convert.ToDecimal(entry.ToArray()[6]),
                                LOW = Convert.ToDecimal(entry.ToArray()[7]),
                                HIGH = Convert.ToDecimal(entry.ToArray()[8]),
                                LEGALCLOSEPRICE = Convert.ToDecimal(entry.ToArray()[9]),
                                WAPRICE = Convert.ToDecimal(entry.ToArray()[10]),
                                CLOSE = Convert.ToDecimal(entry.ToArray()[11])
                            });
                            _context.SaveChanges();
                        }
                    }
                }

                //Console.ReadLine();
            }
        }
    }
}
