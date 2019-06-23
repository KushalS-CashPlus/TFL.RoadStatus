using System;
using System.Threading.Tasks;
using TFL.RoadStatus.Queries;

namespace TFL.RoadStatus.Services
{
    public class RoadStatusService : IRoadStatusService
    {
        private readonly IRoadStatusQuery _roadStatusQuery;

        public RoadStatusService(IRoadStatusQuery roadStatusQuery)
        {
            _roadStatusQuery = roadStatusQuery;
        }

        public async Task Execute(string road)
        {
            var roadStatuses = await _roadStatusQuery.Get(road);

            roadStatuses.ForEach(roadStatus =>
            {
                Console.WriteLine($"The status of the {roadStatus.DisplayName} is as follows");
                Console.WriteLine($"Road Status is {roadStatus.StatusSeverity}");
                Console.WriteLine($"Road Status Description is {roadStatus.StatusSeverityDescription}");
            });
        }
    }

    public interface IRoadStatusService
    {
        Task Execute(string road);
    }
}
