using moex.JSON_class;
using System.IO;
using System.Text.Json;

namespace moex
{
    public class JsonCreator
    {
        public Root Deserialize(string url, string json, string postfix, int i)
        {
            Uri uri = new Uri();
            var url_param = uri.ConcatenateUrlStart(url, json, postfix, i);
            var streamReader = uri.GetStreamFromUrl(url_param);
            var sLineTotal = uri.PageContentFromStream(streamReader);
            return JsonSerializer.Deserialize<Root>(sLineTotal);
        }

        public Root Deserialize(string url, string secId, string json, string postfix, string date)
        {
            Uri uri = new Uri();
            var url_param = uri.ConcatenateUrlFrom(url, secId, json, postfix, date);
            var streamReader = uri.GetStreamFromUrl(url_param);
            var sLineTotal = uri.PageContentFromStream(streamReader);
            return JsonSerializer.Deserialize<Root>(sLineTotal);
        }
    }
}
