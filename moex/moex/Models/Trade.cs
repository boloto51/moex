using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace moex.Models
{
    [Table("trade")]
    public class Trade
    {
        [Column("TRADEDATE", Order = 0)]
        [Key]
        public DateTime TRADEDATE { get; set; }
        [Column("SECID", Order = 1)]
        [Key]
        public string SECID { get; set; }
        [Column("CLOSE")]
        public decimal? CLOSE { get; set; }

        public virtual Security Security { get; set; }
    }
}
