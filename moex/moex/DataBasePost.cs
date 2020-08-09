using System;
using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace moex
{
    public class DataBasePost
    {
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

        public async void ToTradeTableAsync(Root root)
        {
            await Task.Run(() => ToTradeTable(root));
        }

        public void ToTradeTable(Root root)
        {
            DataContext _context = new DataContext();

            foreach (var item in root.history.data)
            {
                var tradeDateFromUrl = item[1].ToString();
                var secIdFromUrl = item[3].ToString();

                var tradeDateFromDB = _context.Trades.Where(t => t.SECID == secIdFromUrl &&
                    t.TRADEDATE.ToString() == tradeDateFromUrl)
                    .Select(t => t.TRADEDATE).FirstOrDefault().ToString();

                if (!String.IsNullOrWhiteSpace(item.ToString()) && String.IsNullOrEmpty(tradeDateFromDB))
                {
                    var close = item[11] == null ? null : item[11].ToString();

                    _context.Trades.Add(new Trade
                    {
                        TRADEDATE = DateTime.Parse(item[1].ToString()).Date,
                        SECID = item[3].ToString(),
                        CLOSE = String.IsNullOrWhiteSpace(close) ?
                            (decimal?)null : Convert.ToDecimal(close.Replace(".", ","))
                    });

                }
            }
            _context.SaveChanges();
        }
    }
}
