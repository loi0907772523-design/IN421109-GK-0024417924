using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private static readonly List<Event> _events = new();
    private static readonly object _lock = new();

    public Task AddAsync(Event newEvent)
    {
        lock (_lock)
        {
            _events.Add(newEvent);
        }

        return Task.CompletedTask;
    }

    public Task<List<Event>> GetByClubIdAsync(Guid clubId)
    {
        lock (_lock)
        {
            return Task.FromResult(
                _events.Where(e => e.ClubId == clubId).ToList());
        }
    }

    public Task<Event?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            return Task.FromResult(
                _events.FirstOrDefault(e => e.Id == id));
        }
    }

    public Task DeleteAsync(Guid id)
    {
        lock (_lock)
        {
            var item = _events.FirstOrDefault(e => e.Id == id);

            if (item == null)
                throw new KeyNotFoundException(
                    "Không tìm thấy sự kiện.");

            _events.Remove(item);
        }

        return Task.CompletedTask;
    }
}