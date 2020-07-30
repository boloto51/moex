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
        [Column("SHORTNAME")]
        public string SHORTNAME { get; set; }
        [Column("SECIDstr")]
        public string SECIDstr { get; set; }
        [Column("SecId")]
        public int? SecId { get; set; }
        [Column("OPEN")]
        public string OPEN { get; set; }
        [Column("LOW")]
        public string LOW { get; set; }
        [Column("HIGH")]
        public string HIGH { get; set; }
        [Column("WAPRICE")]
        public string WAPRICE { get; set; }
        [Column("CLOSE")]
        public string CLOSE { get; set; }

        //public virtual Security Security { get; set; }
    }
}
