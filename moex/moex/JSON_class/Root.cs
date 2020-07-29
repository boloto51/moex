using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace moex.JSON_class
{
    public class Root
    {
        public History history { get; set; }

        [JsonPropertyName("history.cursor")]
        public HistoryCursor history_cursor { get; set; }
    }
}
