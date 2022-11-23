using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostServiceApp
{
    public class StartUp : IHostedService
    {
        private readonly IHostApplicationLifetime _host;
        private ILogger<StartUp> _logger;

        public StartUp(IHostApplicationLifetime host, ILogger<StartUp> logger)
        {
            _host = host;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("start async");
            //Environment.ExitCode = 111111;
            //_host.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("stop async");
            _logger.LogInformation($"Environment.ExitCode: {Environment.ExitCode}");
            return Task.CompletedTask;
        }
    }
}
