
using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Factories;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký Repository
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Đăng ký dịch vụ thông báo
builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SmsNotificationService>();

// Mặc định sử dụng Email
builder.Services.AddScoped<INotificationService, EmailNotificationService>();

// Đăng ký Factory và EventService
builder.Services.AddScoped<NotificationFactory>();
builder.Services.AddScoped<EventService>();

// Đăng ký Controller
builder.Services.AddControllers();

// Đăng ký Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Bật Swagger trong môi trường Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
