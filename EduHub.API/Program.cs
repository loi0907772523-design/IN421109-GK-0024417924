using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Factories;

var builder = WebApplication.CreateBuilder(args);
// Đăng ký Repository
builder.Services.AddScoped<
    IEventRepository,
    InMemoryEventRepository>();

// Đăng ký dịch vụ thông báo
builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SmsNotificationService>();

// Mặc định sử dụng Email
builder.Services.AddScoped<
    INotificationService,
    EmailNotificationService>();

// Đăng ký Factory và EventService
builder.Services.AddScoped<NotificationFactory>();
builder.Services.AddScoped<EventService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
