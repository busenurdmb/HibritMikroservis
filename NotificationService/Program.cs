using NotificationService.Consumers;
using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<UserVerifiedConsumer>();
builder.Services.AddScoped<WelcomeMailService>();

var app = builder.Build();
app.Run();
