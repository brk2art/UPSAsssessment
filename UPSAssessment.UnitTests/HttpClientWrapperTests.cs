using AutoFixture;
using System.Net.Http.Headers;
using UPSAssessment.Business.Services;

namespace UPSAssessment.UnitTests
{
    public class HttpClientWrapperTests
    {
        private readonly Fixture _fixture;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public HttpClientWrapperTests()
        {
            _fixture = new Fixture();
            _httpClient = new HttpClient();
            var token = _fixture.Create<string>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClientWrapper = new HttpClientWrapper();
        }

        [Fact]
        public void Get_Test()
        {
            var request = _fixture.Create<HttpRequestMessage>();
            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            var actual = _httpClientWrapper.Get(request);

            Assert.True(actual == response.Content);
        }

        [Fact]
        public void Post_Test()
        {
            var uri = _fixture.Create<Uri>();
            var content = _fixture.Create<HttpContent>();

            var response = _httpClient.PostAsync(uri, content).Result;
            response.EnsureSuccessStatusCode();

            var actual = _httpClientWrapper.Post(uri, content);

            Assert.True(actual == response);
        }
    }
}
