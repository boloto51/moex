using System;
using System.IO;
using System.Net.Http;
using moex.JSON_class;
using System.Text.Json;

namespace moex
{
    public class Uri
    {
        public string ConcatenateUrlStart(string url, string json, string postfix, int i = 0)
        {
            return (url + json + postfix + Convert.ToString(i * 100));
        }

        public string ConcatenateUrlFrom(string url, string secId, string json, string postfix, string data)
        {
            return (url + "/" + secId + json + postfix + data);
        }

        public StreamReader GetStreamFromUrl(string url)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            var httpGet = httpClient.GetAsync(url);
            var stream = httpGet.Result.Content.ReadAsStreamAsync();
            StreamReader objReader = new StreamReader(stream.Result);
            return objReader;
        }

        public int GetCountHundredsPages(StreamReader streamReader)
        {
            var sLineTotal = PageContentFromStream(streamReader);
            Root root = JsonSerializer.Deserialize<Root>(sLineTotal);
            return (int)Math.Truncate(Convert.ToDecimal(root.history_cursor.data[0][1] / 100));
        }

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

        public DateTime GetPageLastData(StreamReader streamReader)
        {
            var sLineTotal = PageContentFromStream(streamReader);
            Root root = JsonSerializer.Deserialize<Root>(sLineTotal);
            var count = root.history.data.Count;
            return DateTime.Parse(root.history.data[count - 1][1].ToString());
        }
    }
}
