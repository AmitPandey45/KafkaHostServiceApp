using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostServiceApp
{
    public class KafkaProducerHostedService : IHostedService
    {
        private readonly ILogger<KafkaProducerHostedService> logger;
        private IProducer<Null, string> producer;

        public KafkaProducerHostedService(ILogger<KafkaProducerHostedService> logger)
        {
            this.logger = logger;
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                string value = $"Hello World {i}";
                logger.LogInformation(value);
                await producer.ProduceAsync("demo", new Message<Null, string>
                {
                    Value = value,
                }, cancellationToken);
            }

            producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
