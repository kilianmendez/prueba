using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<ActionResult<EventDto>> Create([FromForm] EventCreateDto dto)
    {

        Guid? userId = null;
        var claim = User.FindFirst("id")?.Value;
        if (claim != null && Guid.TryParse(claim, out var uid))
            userId = uid;

        var created = await _eventService.CreateAsync(dto);

        return Ok(created);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
    {
        Guid? userId = null;
        var claim = User.FindFirst("id")?.Value;
        if (claim != null && Guid.TryParse(claim, out var uid))
            userId = uid;

        var list = await _eventService.GetAllAsync(userId);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetById(Guid id)
    {
        Guid? userId = null;
        var claim = User.FindFirst("id")?.Value;
        if (claim != null && Guid.TryParse(claim, out var uid))
            userId = uid;

        var ev = await _eventService.GetByIdAsync(id, userId);
        if (ev == null) return NotFound();
        return Ok(ev);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _eventService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/join")]
    public async Task<IActionResult> Join(Guid id, Guid userId)
    {
        var success = await _eventService.JoinAsync(id, userId); 
        if (!success)
            return BadRequest("No se pudo unir al evento.");

        return Ok();
    }

    [HttpPost("{id:guid}/leave")]
    public async Task<IActionResult> Leave(Guid id, Guid userId)
    {

        var success = await _eventService.LeaveAsync(id, userId);;
        if (!success)
            return BadRequest("No se pudo abandonar al evento.");
        return Ok();
    }
}
