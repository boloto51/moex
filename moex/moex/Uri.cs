using System;
using System.IO;
using System.Net.Http;
using moex.JSON_class;
using System.Text.Json;

namespace moex
{
    public class Uri
    {
        public string ConcatenateUrl(string url, string json, string postfix, int i = 0)
        {
            return (url + json + postfix + Convert.ToString(i * 100));
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
            Root obj = JsonSerializer.Deserialize<Root>(sLineTotal);
            return (int)Math.Truncate(Convert.ToDecimal(obj.history_cursor.data[0][1] / 100));
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
    }
}
