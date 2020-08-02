using System;
using moex.DbContext;

namespace moex
{
    class Program
    {
        static void Main(string[] args)
        {
            string url_init = "http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/tqbr/securities";

            TableSecurity tableSecurity = new TableSecurity();
            tableSecurity.Fill(url_init);
        }
    }
}
