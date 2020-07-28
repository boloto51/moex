using System;
using System.Collections.Generic;
using System.Text;

namespace moex.JSON_class
{
    public class HistoryCursor
    {
        public Metadata2 metadata { get; set; }
        public List<string> columns { get; set; }
        public List<List<int>> data { get; set; }
    }
}
