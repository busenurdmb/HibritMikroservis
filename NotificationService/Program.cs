using BuildingBlocks.Mail;
using Microsoft.EntityFrameworkCore;
using NotificationService.Consumers;
using NotificationService.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<UserVerifiedConsumer>();
builder.Services.AddScoped<WelcomeMailService>();

builder.Services.AddScoped<IMailSender, ConsoleMailSender>();

var app = builder.Build();
app.Run();
