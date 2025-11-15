namespace BackgroundServiceDotNet9.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;

        public MyBackgroundService(ILogger<MyBackgroundService> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); // Garantia que o método não iria bloquear a main thread.

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
