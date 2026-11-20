using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _repository;
    private readonly EventService _eventService;
    private readonly NotificationFactory _factory;

    public EventsController(
        IEventRepository repository,
        EventService eventService,
        NotificationFactory factory)
    {
        _repository = repository;
        _eventService = eventService;
        _factory = factory;
    }

    // GET /api/clubs/{clubId}/events
    [HttpGet("api/clubs/{clubId:guid}/events")]
    public async Task<IActionResult> GetByClub(Guid clubId)
    {
        var events = await _repository.GetByClubIdAsync(clubId);
        return Ok(events);
    }

    // GET /api/events/{id}
    [HttpGet("api/events/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);

        return item == null ? NotFound() : Ok(item);
    }

    // POST /api/clubs/{clubId}/events
    [HttpPost("api/clubs/{clubId:guid}/events")]
    public async Task<IActionResult> Create(
        Guid clubId,
        [FromBody] CreateEventRequest request)
    {
        try
        {
            // Kiểm tra kênh thông báo
            var notification = _factory.Create(request.Channel);

            var item = await _eventService.CreateEventAsync(
                clubId,
                request.Title,
                request.Date,
                request.Capacity,
                notification);

            return CreatedAtAction(
                nameof(GetById),
                new { id = item.Id },
                item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE /api/events/{id}
    [HttpDelete("api/events/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

public record CreateEventRequest(
    string Title,
    DateTime Date,
    int Capacity,
    string Channel);