using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TFL.RoadStatus.Factories;
using TFL.RoadStatus.Models;

namespace TFL.RoadStatus.Queries
{
    public class RoadStatusQuery : IRoadStatusQuery
    {
        private readonly ITflHttpClientFactory _tflHttpClientFactory;

        public RoadStatusQuery(ITflHttpClientFactory tflHttpClientFactory)
        {
            _tflHttpClientFactory = tflHttpClientFactory;
        }

        public async Task<List<RoadStatusModel>> Get(string road)
        {
            using (var response = await _tflHttpClientFactory.Create().GetAsync($"road/{road}").ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoadStatusModel>>(json);
            }
        }
    }

    public interface IRoadStatusQuery
    {
        Task<List<RoadStatusModel>> Get(string road);
    }
}
