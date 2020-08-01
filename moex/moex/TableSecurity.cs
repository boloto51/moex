using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    public class TableSecurity
    {
        //public async void FillingDB(Security secItem, string Url_Security, string Url_Security_Postfix)
        //{
        //    var secId = secItem.SecId;
        //    var _secId = secItem.Id;

        //    var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix);
        //    var sLineTotal = new JsonCreator().Create(objReader);
        //    Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

        //    int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100));

        //    for (int i = 0; i <= count; i++)
        //    {
        //        await Task.Run(() => ParsingPage(Url_Security, Url_Security_Postfix, i, secId, _secId));
        //    }

        //    Console.ReadLine();
        //}

        //public void ParsingPage(string Url_Security, string Url_Security_Postfix, int i, string secId, int _secId)
        //{
        //    var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix, i);
        //    var sLineTotal = new JsonCreator().Create(objReader);

        //    var obj = JsonSerializer.Deserialize<Root>(sLineTotal);

        //    var url = new StreamReaderFromUrl().ConcatenateUrl(Url_Security, Url_Security_Postfix, i, secId);

        //    foreach (var entry in obj.history.data)
        //    {
        //        Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", entry.ToArray()[3], entry.ToArray()[2]);

        //        DataContext _context = new DataContext();

        //        if (_context.Securities.Select(a => a.SecId == entry.ToArray()[3].ToString()) != null)
        //        {
        //            _context.Securities.Add(new Security
        //            {
        //                SecId = entry.ToArray()[3].ToString(),
        //                ShortName = entry.ToArray()[2].ToString()
        //            });
        //            _context.SaveChanges();
        //        }
        //    }
        //}
    }
}
