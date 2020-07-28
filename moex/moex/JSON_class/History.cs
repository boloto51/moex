using System;
using System.Collections.Generic;
using System.Text;

namespace moex.JSON_class
{
    public class History
    {
        public Metadata metadata { get; set; }
        public List<string> columns { get; set; }
        public List<object> data { get; set; }
    }
}
