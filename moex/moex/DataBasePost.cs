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
                //Console.WriteLine("TRADEDATE: {0}\tSECID: {1}\tCLOSE: {2}",
                //    item.ToArray()[1], item.ToArray()[3], item.ToArray()[11]);

                //var secId = _context.Securities.Where(a => a.SECID == item.ToArray()[3].ToString())
                //    .Select(a => a.Id).FirstOrDefault();

                var close = item[11] as string;

                //if (_context.Trades.Where(a => a.SECID == secId).Select(a => a.Id).FirstOrDefault() == null)
                //{
                _context.Trades.Add(new Trade
                {
                    TRADEDATE = DateTime.Parse(item[1].ToString()).Date,
                    SECID = item[3].ToString(),
                    CLOSE = String.IsNullOrWhiteSpace(close) ?
                        (decimal?)null : Convert.ToDecimal(close.Replace(".", ","))
                });

                //Console.WriteLine("TRADEDATE: {0}\tSECIDstr: {1}\tSECID: {2}\tCLOSE: {3}",
                //    DateTime.Parse(item.ToArray()[1].ToString()), item.ToArray()[3].ToString(), secId, String.IsNullOrEmpty(item.ToList()[11].ToString()) ?
                //        (decimal?)null : Convert.ToDecimal(item.ToArray()[11].ToString()));

                //}
            }
            _context.SaveChanges();
        }
    }
}
