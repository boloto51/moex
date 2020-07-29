using System;
using System.Collections.Generic;
using System.Text;

namespace moex.JSON_class
{
    public class Root
    {
        public History history { get; set; }
        public HistoryCursor history_cursor { get; set; }
    }
}
