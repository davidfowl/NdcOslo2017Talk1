using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyRazorPagesApp
{
    public class RequestCounter : IHostedService
    {
        private readonly ILogger<RequestCounter> _logger;
        private readonly RequestCount _count;

        private Task _running;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public RequestCounter(RequestCount count, ILogger<RequestCounter> logger)
        {
            _count = count;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation("Starting background request counter logging");

            _running = LogRequests(_cts.Token);
        }

        public async Task LogRequests(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("The request count is {count}", _count.NumberOfRequests);
                    _count.Reset();
                    await Task.Delay(5000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Stopped
            }
        }

        public void Stop()
        {
            _logger.LogInformation("Stopping background request counter logging");
            _cts.Cancel();
            _running.GetAwaiter().GetResult();
            _logger.LogInformation("Stopped background request counter logging");
        }
    }
}