using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}