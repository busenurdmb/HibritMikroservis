using BuildingBlocks.Mail;
using Microsoft.EntityFrameworkCore;
using NotificationService.Consumers;
using NotificationService.Infrastructure;
using MediatR;

using BuildingBlocks.Mail.Smtp;
using NotificationService.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<UserVerifiedConsumer>();
builder.Services.AddScoped<WelcomeMailService>();




builder.Services.AddApplicationService(builder.Configuration);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddSingleton<IMailSender, SmtpMailSender>();



var app = builder.Build();
app.Run();
