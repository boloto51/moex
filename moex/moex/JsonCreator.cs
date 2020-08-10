using moex.JSON_class;
using moex.Services;

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
            return httpService.GetAsync1<Root>(url_param).Result;
        }
    }
}
