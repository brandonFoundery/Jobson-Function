namespace UpworkJobPostingTest;

public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly string _url;
    private readonly string _response;

    public MockHttpMessageHandler(string url, string response)
    {
        _url = url;
        _response = response;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        if (request.RequestUri.AbsoluteUri == _url)
        {
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(_response)
            };
        }
        else
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }
    }
}