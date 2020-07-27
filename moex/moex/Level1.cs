using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace moex
{
    public class Level1
    {
        [JsonProperty("history")]
        public string history { get; set; }

        [JsonProperty("history_cursor")]
        public string history_cursor { get; set; }
    }
}
