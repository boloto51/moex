using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace moex.Models
{
    [Table("trade")]
    public class Trade
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("BOARDID")]
        public string BOARDID { get; set; }
        [Column("TradeDate")]
        public string TradeDate { get; set; }
        [Column("SecId")]
        public int SecId { get; set; }
        [Column("OPEN")]
        public decimal OPEN { get; set; }
        [Column("LOW")]
        public decimal LOW { get; set; }
        [Column("HIGH")]
        public decimal HIGH { get; set; }
        [Column("LEGALCLOSEPRICE ")]
        public decimal LEGALCLOSEPRICE { get; set; }
        [Column("WAPRICE")]
        public decimal WAPRICE { get; set; }
        [Column("CLOSE")]
        public decimal CLOSE { get; set; }
    }
}
