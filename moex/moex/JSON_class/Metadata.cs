using System;
using System.Collections.Generic;
using System.Text;

namespace moex.JSON_class
{
    public class Metadata
    {
        public BOARDID BOARDID { get; set; }
        public TRADEDATE TRADEDATE { get; set; }
        public SHORTNAME SHORTNAME { get; set; }
        public SECID SECID { get; set; }
        public NUMTRADES NUMTRADES { get; set; }
        public VALUE VALUE { get; set; }
        public OPEN OPEN { get; set; }
        public LOW LOW { get; set; }
        public HIGH HIGH { get; set; }
        public LEGALCLOSEPRICE LEGALCLOSEPRICE { get; set; }
        public WAPRICE WAPRICE { get; set; }
        public CLOSE CLOSE { get; set; }
        public VOLUME VOLUME { get; set; }
        public MARKETPRICE2 MARKETPRICE2 { get; set; }
        public MARKETPRICE3 MARKETPRICE3 { get; set; }
        public ADMITTEDQUOTE ADMITTEDQUOTE { get; set; }
        public MP2VALTRD MP2VALTRD { get; set; }
        public MARKETPRICE3TRADESVALUE MARKETPRICE3TRADESVALUE { get; set; }
        public ADMITTEDVALUE ADMITTEDVALUE { get; set; }
        public WAVAL WAVAL { get; set; }
        public TRADINGSESSION TRADINGSESSION { get; set; }

    }
}
