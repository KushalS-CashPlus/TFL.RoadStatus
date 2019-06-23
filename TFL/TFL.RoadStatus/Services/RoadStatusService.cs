using System;
using System.Threading.Tasks;

namespace TFL.RoadStatus.Services
{
    public class RoadStatusService : IRoadStatusService
    {
        public RoadStatusService()
        {
        }

        public Task Execute(string road)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRoadStatusService
    {
        Task Execute(string road);
    }
}
