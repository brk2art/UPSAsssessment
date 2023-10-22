using AutoFixture;
using Moq;
using System.Text.Json;
using UPSAssessment.Business.DTOs;
using UPSAssessment.Business.Services;

namespace UPSAssessment.UnitTests
{
    public class EmployeeServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IHttpClientWrapper> _httpClientWrapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _fixture = new Fixture();
            _httpClientWrapper = new Mock<IHttpClientWrapper>();
            _employeeService = new EmployeeService(_httpClientWrapper.Object);
        }

        [Fact]
        public void GetAll_Test()
        {
            var content = _fixture.Create<HttpContent>();
            var httpRequestMessage = _fixture.Create<HttpRequestMessage>();
            _httpClientWrapper.Setup(x => x.Get(httpRequestMessage)).Returns(content);

            var jsonResult = JsonDocument.Parse(content.ReadAsStringAsync().Result);
            var json = jsonResult.RootElement.GetRawText();
            var result = JsonSerializer.Deserialize<List<EmployeeDto>>(json) ?? new List<EmployeeDto>();

            var actual = _employeeService.GetAll();

            Assert.True(actual.Count == result.Count);
        }

        [Fact]
        public void Add_Test()
        {
            var employee = _fixture.Create<EmployeeDto>();
            var content = _fixture.Create<HttpContent>();
            var uri = _fixture.Create<Uri>();
            var httpResponseMessage = _fixture.Create<HttpResponseMessage>();
            _httpClientWrapper.Setup(x => x.Post(uri, content)).Returns(httpResponseMessage);

            var jsonResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var result = !string.IsNullOrEmpty(jsonResult);

            var actual = _employeeService.Add(employee);

            Assert.True(actual == result);
        }
    }
}
