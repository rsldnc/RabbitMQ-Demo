// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using FormulaAirline.TicketProcessing;

Console.WriteLine("Welcome to the ticketing service");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "mypass",
    VirtualHost = "/"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "bookings",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    var booking = JsonSerializer.Deserialize<Booking>(message);

    Console.WriteLine($"{booking.PassportNumber} holder {booking.PassengerName} will fly");
    Console.WriteLine($"From {booking.From} to {booking.To}");
};

channel.BasicConsume(queue: "bookings", true, consumer);

Console.ReadKey();