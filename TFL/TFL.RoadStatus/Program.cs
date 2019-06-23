using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TFL.RoadStatus.Factories;
using TFL.RoadStatus.Queries;
using TFL.RoadStatus.Services;

namespace TFL.RoadStatus
{
    class Program
    {
        static IHost host;

        public static async Task Main(string[] args)
        {
            host = new HostBuilder()
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            config.AddCommandLine(args);
                        })
                        .ConfigureLogging(logging =>
                        {
                            logging.SetMinimumLevel(LogLevel.Error);
                        })
                        .ConfigureServices((hostContext, services) =>
                        {
                            services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                            services.AddScoped<IRoadStatusQuery, RoadStatusQuery>();
                            services.AddScoped<IRoadStatusService, RoadStatusService>();
                            services.AddSingleton<ITflHttpClientFactory, TflHttpClientFactory>();
                            services.AddHostedService<TFLHost>();
                        })
                        .Build();

            await host.RunAsync();
        }
    }
}
