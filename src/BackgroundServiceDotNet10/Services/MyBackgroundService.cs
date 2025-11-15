namespace BackgroundServiceDotNet10.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;

        public MyBackgroundService(ILogger<MyBackgroundService> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Agora todo esse método roda em uma thread separada automaticamente (no .NET 10).

            while (!stoppingToken.IsCancellationRequested)
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
