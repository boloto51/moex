using System;
using System.Linq;
using System.Threading.Tasks;
using moex.DbContext;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext _context = new DataContext();

            string url_init = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities.json";
            string postfix_start = "?start=";

            Uri uri = new Uri();
            var url_postfix = uri.ConcatenateUrl(url_init, postfix_start);
            var streamReader = uri.GetStreamFromUrl(url_postfix);
            var countHundredsPages = uri.GetCountHundredsPages(streamReader);

            JsonCreator jsonCreator = new JsonCreator();

            for (int i=0; i <= countHundredsPages; i++)
            {
                var root = jsonCreator.Deserialize(url_init, postfix_start, i);
                jsonCreator.WritingToSecurityTable(root);
            }

            Console.ReadLine();
        }
    }
}
