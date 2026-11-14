using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public class SmsNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}