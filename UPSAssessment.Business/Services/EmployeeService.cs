using System.Net.Http.Headers;
using System.Text.Json;
using UPSAssessment.Business.DTOs;

namespace UPSAssessment.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private const string EmployeeUrl = "https://gorest.co.in/public/v2/users";
        private const string MediaType = "application/json";
        private readonly IHttpClientWrapper _httpClient;

        public EmployeeService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public List<EmployeeDto> GetAll(string name = "")
        {
            var json = GetJsonResult(string.IsNullOrEmpty(name) ? "" : $"?name={name}");
            return JsonSerializer.Deserialize<List<EmployeeDto>>(json) ?? new List<EmployeeDto>();
        }

        public bool Add(EmployeeDto employee)
        {
            var content = GetContent(employee);
            var response = _httpClient.Post(new Uri(EmployeeUrl), content);
            var result = response.Content.ReadAsStringAsync().Result;
            return !string.IsNullOrEmpty(result);
        }

        private string GetJsonResult(string parameter = "")
        {
            var request = GetRequest(new Uri(EmployeeUrl + parameter), MediaType);
            var content = _httpClient.Get(request);

            var result = JsonDocument.Parse(content.ReadAsStringAsync().Result);
            return result.RootElement.GetRawText();
        }

        private HttpContent GetContent(EmployeeDto employee)
        {
            var content = new StringContent(JsonSerializer.Serialize(employee));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return content;
        }

        private HttpRequestMessage GetRequest(Uri uri, string mediaType)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            return request;
        }
    }
}
