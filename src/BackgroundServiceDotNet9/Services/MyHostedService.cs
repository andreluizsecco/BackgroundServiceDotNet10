using System.Reflection;

namespace BackgroundServiceDotNet9.Services
{
    public class MyHostedService : IHostedService
    {
        private readonly ILogger<MyHostedService> _logger;

        public MyHostedService(ILogger<MyHostedService> logger) =>
            _logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Message}",
                "Background Service initializing");

            // Inicia a tarefa em segundo plano, não imapactando a thread principal.
            Task.Factory.StartNew(async () => await ExecuteAsync(cancellationToken), TaskCreationOptions.LongRunning);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Message}",
                "Background Service stopped");

            return Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("{DateTime}: {Message}",
                    DateTimeOffset.UtcNow,
                    "Background Service is running");

                Thread.Sleep(TimeSpan.FromSeconds(2));
                //await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
