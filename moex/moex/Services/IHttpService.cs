using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace moex.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> DeleteAsync(string serviceUrl, long? value);
        Task<HttpResponseMessage> GetAsync(string serviceUrl, long? value = null);
        Task<T> GetAsync1<T>(string serviceUrl, long? value = null);
        Task<HttpResponseMessage> PostAsync(string serviceUrl, object value);
        Task<HttpResponseMessage> PutAsync(string serviceUrl, object model);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        Task<HttpResponseMessage> PostXmlAsync(string serviceUrl, string value);
        void SetAuthorization(string credentials);
    }
}
