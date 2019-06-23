using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TFL.RoadStatus.Tests
{
    public class FakeNotFoundHttpHandler : HttpMessageHandler
    {
        public HttpRequestMessage RequestMessage { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestMessage = request;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}
