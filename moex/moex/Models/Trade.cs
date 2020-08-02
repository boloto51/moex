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
        [Column("TradeDate")]
        public DateTime TradeDate { get; set; }
        [Column("SECIDstr")]
        public string SECIDstr { get; set; }
        [Column("SECID")]
        public int SECID { get; set; }
        [Column("CLOSE")]
        public decimal? CLOSE { get; set; }
    }
}
