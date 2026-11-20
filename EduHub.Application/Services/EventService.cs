using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Services;

public class EventService
{
    private readonly IEventRepository _repository;
    private readonly INotificationService _notification;

    public EventService(
        IEventRepository repository,
        INotificationService notification)
    {
        _repository = repository;
        _notification = notification;
    }

    public async Task<Event> CreateEventAsync(
        Guid clubId,
        string title,
        DateTime date,
        int capacity,
        INotificationService? notification = null)
    {
        var newEvent = Event.Create(
            clubId, title, date, capacity);

        await _repository.AddAsync(newEvent);

        (notification ?? _notification)
            .Send($"Sự kiện mới: {title}");

        return newEvent;
    }
}