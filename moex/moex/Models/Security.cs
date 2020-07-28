using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace moex.Models
{
    [Table("security")]
    public class Security
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("SecId")]
        public string SecId { get; set; }
        [Column("ShortName")]
        public string ShortName { get; set; }
    }
}
