using BuildingBlocks.EventBus;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Application.Commands;
using IdentityService.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(typeof(Program)); // CQRS için

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

builder.Services.AddSingleton<IEventPublisher, RabbitMQEventPublisher>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "identity API v1");
        c.RoutePrefix = "swagger"; // "/swagger" yolunu kullan
    });
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();

