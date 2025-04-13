using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using SharedKernel.Events;


namespace NotificationService.Consumers;

public class UserVerifiedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public UserVerifiedConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5673
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var queueName = nameof(UserVerifiedIntegrationEvent);
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var @event = JsonSerializer.Deserialize<UserVerifiedIntegrationEvent>(json);

            if (@event is not null)
            {
                using var scope = _serviceProvider.CreateScope();
                var mailService = scope.ServiceProvider.GetRequiredService<WelcomeMailService>();
                await mailService.SendWelcomeMail(@event.Email);
            }
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
