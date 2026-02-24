using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.BackgroundServices
{
    public class BloodRequestExpirationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(10);

        public BloodRequestExpirationWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("BloodRequestExpirationWorker started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var requesterService = scope.ServiceProvider.GetRequiredService<IRequesterService>();

                    try
                    {
                        await requesterService.MarkExpiredRequestsAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}