namespace UPSAssessment.Business.Services
{
    public interface IHttpClientWrapper
    {
        HttpContent Get(HttpRequestMessage request);

        HttpResponseMessage Post(Uri requestUri, HttpContent content);
    }
}
