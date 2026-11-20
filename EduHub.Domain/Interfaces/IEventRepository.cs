using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

public interface IEventRepository
{
    Task AddAsync(Event newEvent);

    Task<List<Event>> GetByClubIdAsync(Guid clubId);

    Task<Event?> GetByIdAsync(Guid id);

    Task DeleteAsync(Guid id);
}