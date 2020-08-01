using moex.JSON_class;
using moex.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    class FillingDataBase
    {
        //public async void FillingDB(Security secItem, string Url_Security, string Url_Security_Postfix, string typeObj)
        //{
        //    var secId = secItem.SecId;
        //    var _secId = secItem.Id;

        //    var objReader = new StreamReaderFromUrl().Read(Url_Security, Url_Security_Postfix);
        //    var sLineTotal = new JsonCreator().Create(objReader);
        //    Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);

        //    int count = (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100));

        //    TableSecurity tableSecurity = new TableSecurity();
        //    TableTrade tableTrade = new TableTrade();

        //    if (typeObj == "securityTable")
        //    {
        //        for (int i = 0; i <= count; i++)
        //        {
        //            await Task.Run(() => tableSecurity.ParsingPage(Url_Security, Url_Security_Postfix, i, secId, _secId));
        //        }
        //    }

        //    if (typeObj == "securityTable")
        //    {
        //        for (int i = 0; i <= count; i++)
        //        {
        //            await Task.Run(() => tableTrade.ParsingPage(Url_Security, Url_Security_Postfix, i, secId, _secId));
        //        }
        //    }

        //    Console.ReadLine();
        //}
    }
}
