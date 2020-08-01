using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace moex
{
    public class JsonCreator
    {
        //public string Create (StreamReader objReader)
        //{
        //    string sLine = "";
        //    string sLineTotal = "";
        //    int j = 0;

        //    while (sLine != null)
        //    {
        //        j++;
        //        sLine = objReader.ReadLine();
        //        if (sLine != null)
        //            sLineTotal = sLineTotal + sLine.Trim();
        //    }

        //    return sLineTotal;
        //}

        public Root Deserialize(string url, string postfix, int i)
        {
            Uri uri = new Uri();
            var url_param = uri.ConcatenateUrl(url, postfix, i);
            var streamReader = uri.GetStreamFromUrl(url_param);
            var sLineTotal = uri.PageContentFromStream(streamReader);
            return JsonSerializer.Deserialize<Root>(sLineTotal);
        }

        public void WritingToSecurityTable(Root root)
        {
            DataContext _context = new DataContext();

            foreach (var item in root.history.data)
            {
                Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", item.ToArray()[3], item.ToArray()[2]);

                if (_context.Securities.Select(a => a.SecId == item.ToArray()[3].ToString()) != null)
                {
                    _context.Securities.Add(new Security
                    {
                        SecId = item.ToArray()[3].ToString(),
                        ShortName = item.ToArray()[2].ToString()
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
