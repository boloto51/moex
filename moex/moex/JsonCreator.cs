using moex.JSON_class;
using moex.Services;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace moex
{
    public class JsonCreator
    {
        Uri uri;
        IHttpService httpService;

        public JsonCreator(Uri _uri, HttpService _httpService)
        {
            uri = _uri;
            httpService = _httpService;
        }
        public Root Deserialize(string url, string json, string postfix, int i)
        {
            
            var url_param = uri.ConcatenateUrlStart(url, json, postfix, i);
            //var streamReader = uri.GetStreamFromUrl(url_param);
            //var sLineTotal = uri.PageContentFromStream(streamReader);
            //return JsonSerializer.Deserialize<Root>(sLineTotal);
            return Task.Run(() => httpService.GetAsync1<Root>(url)).Result;
        }

        public Root Deserialize(string url, string secId, string json, string postfix, string date)
        {
            //Uri uri = new Uri();
            var url_param = uri.ConcatenateUrlFrom(url, secId, json, postfix, date);
            //var streamReader = uri.GetStreamFromUrl(url_param);
            //var sLineTotal = uri.PageContentFromStream(streamReader);
            //return JsonSerializer.Deserialize<Root>(sLineTotal);
            return Task.Run(() => httpService.GetAsync1<Root>(url_param)).Result;
        }
    }
}
