using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace moex.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient HttpClient;

        public HttpService()
        {
            HttpClient = new HttpClient();
        }

        public void SetAuthorization(string credentials)
        {
            HttpClient.DefaultRequestHeaders.Remove("Authorization");
            HttpClient.DefaultRequestHeaders.Add("Authorization", credentials);
        }

        public async Task<HttpResponseMessage> PostAsync(string serviceUrl, object value)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, serviceUrl);
                request.Content =
                    new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
                return await HttpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string serviceUrl, long? value)
        {
            return await HttpClient.DeleteAsync(serviceUrl + "/" + value);
        }

        public async Task<HttpResponseMessage> GetAsync(string serviceUrl, long? value = null)
        {
            if (value == null)
                return await HttpClient.GetAsync(serviceUrl);
            return await HttpClient.GetAsync(serviceUrl + "/" + value);
        }

        public async Task<HttpResponseMessage> PutAsync(string serviceUrl, object model)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, serviceUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            return await HttpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostXmlAsync(string serviceUrl, string value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, serviceUrl);
            request.Content = new StringContent(value, Encoding.UTF8, "application/xml");
            return await HttpClient.SendAsync(request);
        }

        public async Task<T> GetAsync1<T>(string serviceUrl, long? value = null)
        {
            try
            {
                var result = await HttpClient.GetAsync(serviceUrl);
                var content = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content);
            }
            catch
            {
                return default(T);
            }
        }
    }
}

