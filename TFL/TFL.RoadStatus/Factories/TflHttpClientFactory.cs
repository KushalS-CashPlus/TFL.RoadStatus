using System;
using System.Net.Http;
using TFL.RoadStatus.Configurations;
using TFL.RoadStatus.Factories.Handlers;
using static TFL.RoadStatus.Factories.TflHttpClientFactory;

namespace TFL.RoadStatus.Factories
{
    public class TflHttpClientFactory : ITflHttpClientFactory
    {
        private static readonly object _createLock = new object();
        private static HttpClient _httpClient;
        private readonly string _tflApiBaseAddress;

        public TflHttpClientFactory()
        {
            _tflApiBaseAddress = ConfigurationManager.AppSettings["TFL.ApiBaseAddress"];
        }

        public HttpClient Create()
        {
            lock (_createLock)
            {
                if (_httpClient != null)
                    return _httpClient;

                var handler = new TflHttpHandler();
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_tflApiBaseAddress)
                };

                return _httpClient;
            }
        }
    }

    public interface ITflHttpClientFactory
    {
        HttpClient Create();
    }
}
