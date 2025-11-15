namespace BackgroundServiceDotNet10.Services
{
    public class MyBackgroundService2 : BackgroundService
    {
        private readonly ILogger<MyBackgroundService2> _logger;

        public MyBackgroundService2(ILogger<MyBackgroundService2> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var watcher = new FileSystemWatcher("C:\\Temp");
            watcher.EnableRaisingEvents = true;
            watcher.Created += (s, e) =>
            {
                Console.WriteLine($"Arquivo criado: {e.Name}");
            };

            Console.WriteLine("Monitorando C:\\Temp. Pressione Ctrl+C para encerrar.");

            // Para implementações que por si só não mantem um loop de execução, a chamada e método abaixo é uma solução simples e funcional.
            await WaitForCancellationRequest(stoppingToken);
        }

        private async Task WaitForCancellationRequest(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(10000, cancellationToken);
        }
    }
}
