using System;
using moex.DbContext;
using moex.JSON_class;
using moex.Models;
using System.Linq;
using System.Threading.Tasks;

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
                Console.WriteLine("SECID: {0}\tSHORTNAME: {1}", item.ToArray()[3], item.ToArray()[2]);

                if (_context.Securities.Where(a => a.SecId == item.ToArray()[3].ToString())
                    .Select(a => a.SecId).FirstOrDefault() == null)
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
