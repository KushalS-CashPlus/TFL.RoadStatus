using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TFL.RoadStatus.Configurations;

namespace TFL.RoadStatus.Factories.Handlers
{
    public class TflHttpHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(request.RequestUri);
            var requestQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            requestQueryString["app_id"] = ConfigurationManager.AppSettings["TFL.App.Id"];
            requestQueryString["app_key"] = ConfigurationManager.AppSettings["TFL.App.Key"];

            uriBuilder.Query = requestQueryString.ToString();
            request.RequestUri = uriBuilder.Uri;

            return base.SendAsync(request, cancellationToken);
        }
    }
}
