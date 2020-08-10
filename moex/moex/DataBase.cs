using System;
using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace moex
{
    public class DataBase
    {
        public List<Security> FromSecurityTable()
        {
            DataContext context = new DataContext();
            return context.Securities.Select(a => a).ToList();
        }

        public int FromSecurityTableCount()
        {
            DataContext context = new DataContext();
            return context.Securities.Select(a => a).Count();
        }

        public int FromTradeTableCount()
        {
            DataContext context = new DataContext();
            return context.Trades.Select(a => a).Count();
        }

        public async void ToSecurityTableAsync(Root root)
        {
            await Task.Run(() => ToSecurityTable(root));
        }

        public void ToSecurityTable(Root root)
        {
            DataContext _context = new DataContext();

            foreach (var item in root.history.data)
            {
                Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", item[3], item[2]);

                if (_context.Securities.Where(a => a.SECID == item[3].ToString())
                    .Select(a => a.SECID).FirstOrDefault() == null)
                {
                    _context.Securities.Add(new Security
                    {
                        SECID = item[3].ToString(),
                        SHORTNAME = item[2].ToString()
                    });
                }
            }
            _context.SaveChanges();
        }

        public async void ToTradeTableAsync(Root root, string url_init, string secId, string postfix_json, string postfix_from, string date)
        {
            await Task.Run(() => ToTradeTable(root, url_init, secId, postfix_json, postfix_from, date));
            //ToTradeTable(root, url_init, secId, postfix_json, postfix_from, date);
        }

        public void ToTradeTable(Root root, string url_init, string secId, string postfix_json, string postfix_from, string date)
        {
            DataContext _context = new DataContext();

            foreach (var item in root.history.data)
            {
                //var tradeDateFromUrl = item[1].ToString();
                //var secIdFromUrl = item[3].ToString();

                //var tradeDateFromDB = _context.Trades.Where(t => t.SECID == secIdFromUrl &&
                //    t.TRADEDATE.ToString() == tradeDateFromUrl)
                //    .Select(t => t.TRADEDATE).FirstOrDefault().ToString();

                //if (!String.IsNullOrWhiteSpace(item.ToString()) && String.IsNullOrEmpty(tradeDateFromDB))
                //{
                    var close = item[11] == null ? null : item[11].ToString();
                    var _close = String.IsNullOrWhiteSpace(close) ? (decimal?)null : Convert.ToDecimal(close.Replace(".", ","));

                    _context.Trades.Add(new Trade
                    {
                        TRADEDATE = DateTime.Parse(item[1].ToString()).Date,
                        SECID = item[3].ToString(),
                        CLOSE = _close
                        //CLOSE = String.IsNullOrWhiteSpace(close) ?
                        //    (decimal?)null : Convert.ToDecimal(close.Replace(".", ","))
                    });
                Console.WriteLine(DateTime.Parse(item[1].ToString()).Date + "\t" + item[3].ToString() + "\t" + _close);

                //}
            }
            _context.SaveChanges();
        }

        public Trade FindLastDate(string secId)
        {
            DataContext _context = new DataContext();
            //return _context.Trades.Where(t => t.SECID == secId).Select(t => t).Last();
            return _context.Trades.Where(t => t.SECID == secId).OrderBy(t => t.TRADEDATE).Last();
        }

        public async Task<List<Trade>> FindLastTrades(List<Security> secList)
        {
            List<Trade> lastTradesInDB = new List<Trade>();

            foreach (var secItem in secList)
            {
                var trade = await Task.Run(() => FindLastDate(secItem.SECID));
                lastTradesInDB.Add(trade);
            }

            return lastTradesInDB;
        }

        public List<Trade> FindLastTrades()
        {
            DataContext _context = new DataContext();
            return _context.Trades.ToList().GroupBy(t => t.SECID)
                        .Select(g => new Trade()
                        {
                            SECID = g.Key,
                            TRADEDATE = g.Select(t => t.TRADEDATE).LastOrDefault(),
                            CLOSE = g.Select(t => t.CLOSE).LastOrDefault()
                        }).ToList();
        }
    }
}
