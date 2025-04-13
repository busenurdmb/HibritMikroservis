using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BuildingBlocks.EventBus;

public class RabbitMQEventPublisher : IEventPublisher
{
    private readonly IConnection _connection;

    public RabbitMQEventPublisher()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", // Docker'daki rabbitmq
            Port = 5673
           
        };

        _connection = factory.CreateConnection();
    }

    public void Publish<T>(T @event) where T : class
    {
        using var channel = _connection.CreateModel();

        var queueName = typeof(T).Name;

        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

        Console.WriteLine($"[RabbitMQ] Event gönderildi → {queueName}: {message}");
    }
}
