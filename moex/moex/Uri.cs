using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using moex.JSON_class;
using moex.Services;

namespace moex
{
    public class Uri
    {
        HttpService httpService;

        public Uri(HttpService _httpService)
        {
            httpService = _httpService;
        }
        public string ConcatenateUrlStart(string url, string json, string postfix, int i = 0)
        {
            return (url + json + postfix + Convert.ToString(i * 100));
        }

        public string ConcatenateUrlFrom(string url, string secId, string json, string postfix, string date)
        {
            return (url + "/" + secId + json + postfix + date);
        }

        public StreamReader GetStreamFromUrl(string url)
        {
            //var httpClient = new HttpClient();
            //httpClient.Timeout = TimeSpan.FromSeconds(30);
            //var httpGet = httpClient.GetAsync(url);
            var httpClient = new HttpService();
            var httpGet = httpClient.GetAsync(url);
            var stream = httpGet.Result.Content.ReadAsStreamAsync();
            StreamReader objReader = new StreamReader(stream.Result);
            return objReader;
        }

        public int GetCountHundredsPages(string url)
        {
            //Root root = await Task.Run(() => httpService.GetAsync1<Root>(url));
            //Root root = Task.Run(() => httpService.GetAsync1<Root>(url)).Result;
            Root root = httpService.GetAsync1<Root>(url).Result;
            return (int)Math.Truncate(Convert.ToDecimal(root.history_cursor.data[0][1] / 100));
        }

        //public int GetCountHundredsPages(StreamReader streamReader)
        //{
        //    var sLineTotal = PageContentFromStream(streamReader);
        //    Root root = JsonSerializer.Deserialize<Root>(sLineTotal);
        //    return (int)Math.Truncate(Convert.ToDecimal(root.history_cursor.data[0][1] / 100));
        //}

        public string PageContentFromStream(StreamReader objReader)
        {
            string sLine = "";
            string sLineTotal = "";
            int j = 0;

            while (sLine != null)
            {
                j++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    sLineTotal = sLineTotal + sLine.Trim();
            }

            return sLineTotal;
        }

        //public DateTime GetPageLastData(StreamReader streamReader)
        //{
        //    //var sLineTotal = PageContentFromStream(streamReader);
        //    //Root root = JsonSerializer.Deserialize<Root>(sLineTotal);
        //    //var count = root.history.data.Count;
        //    //return DateTime.Parse(root.history.data[count == 0 ? 0 : count - 1][1].ToString());
        //    //return count > 0 ? 
        //    //    DateTime.Parse(root.history.data[count][1].ToString()) :
        //    //    DateTime.Parse(root.history.data[0].ToString());
        //    //return count > 0 ?
        //    //    DateTime.Parse(root.history.data[count][1].ToString()) : (DateTime)null;
        //    Root root = GetPageLastDataRoot(streamReader);
        //    int count = GetPageLastDataCount(root);
        //    return DateTime.Parse(root.history.data[count - 1][1].ToString());
        //}

        //public Root GetPageLastDataRoot(StreamReader streamReader)
        //{
        //    var sLineTotal = PageContentFromStream(streamReader);
        //    return JsonSerializer.Deserialize<Root>(sLineTotal);
        //}

        public int GetPageLastDataCount(Root root)
        {
            return root.history.data.Count;
        }

        public DateTime GetPageLastData(Root root, int count)
        {
            return DateTime.Parse(root.history.data[count - 1][1].ToString());
        }
    }
}
