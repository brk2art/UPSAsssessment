using System.Net.Http.Headers;

namespace UPSAssessment.Business.Services
{
    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private bool _disposed;
        private const string Token = "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023";

        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        public HttpContent Get(HttpRequestMessage request)
        {
            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            return response.Content;
        }

        public HttpResponseMessage Post(Uri requestUri, HttpContent content)
        {
            var response = _httpClient.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            _disposed = true;
            _httpClient?.Dispose();
        }
    }
}
