using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TFL.RoadStatus.Models.Enums;
using TFL.RoadStatus.Services;

namespace TFL.RoadStatus
{
    public class TFLHost : IHostedService
    {
        private readonly IRoadStatusService _roadStatusService;
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly IConfiguration _configuration;

        public TFLHost
        (
            IRoadStatusService roadStatusService,
            IApplicationLifetime applicationLifetime,
            IConfiguration configuration
        )
        {
            _roadStatusService = roadStatusService;
            _applicationLifetime = applicationLifetime;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(OnStarted);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            var road = _configuration["road"];
            Task.Run(async () =>
            {
                try
                {
                    await _roadStatusService.Execute(road);
                    Environment.Exit((int)ExitCode.Success);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine($"{road} is not a valid road");
                    Environment.Exit((int)ExitCode.Error);
                }
            });
        }
    }
}
