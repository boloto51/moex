using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moex.Models
{
    [Table("security")]
    public class Security
    {
        [Column("SECID")]
        [Key]
        public string SECID { get; set; }
        [Column("SHORTNAME")]
        public string SHORTNAME { get; set; }

        public virtual ICollection<Trade> Trades { get; set; }
    }
}
