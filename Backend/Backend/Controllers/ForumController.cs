using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Models.Interfaces;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumController : ControllerBase
{
    private readonly IForumService _forumService;
    private readonly SmartSearchService _smartSearchService;

    public ForumController(IForumService forumService, SmartSearchService smartSearchService)
    {
        _forumService = forumService;
        _smartSearchService = smartSearchService;
    }

    [HttpPost("forum")]
    public async Task<IActionResult> CreateForum([FromBody] CreateForumDTO forum)
    {
        if (forum == null)
        {
            return BadRequest(new { message = "Los datos del foro no pueden ser nulos." });
        }

        bool creado = await _forumService.CreateForumAsync(forum);

        if (creado)
        {
            return Ok(new { message = "Foro creado correctamente." });
        }

        return StatusCode(500, "Ocurrió un error al crear el foro.");
    }

    [HttpGet("forums")]
    public async Task<IActionResult> GetAllForums()
    {
        IEnumerable<ForumDTO> forums = await _forumService.GetAllForumsAsync();
        return Ok(forums);
    }

    [HttpGet("forum")]
    public async Task<IActionResult> GetForumByIdAsync(Guid id)
    {
        var forum = await _forumService.GetForumByIdAsync(id);
        return Ok(forum);
    }

    [HttpPost("thread")]
    public async Task<IActionResult> CreateThread([FromBody] CreateForumThreadDTO threadDto)
    {
        if (threadDto == null)
        {
            return BadRequest(new { message = "Los datos del hilo son requeridos." });
        }

        bool creado = await _forumService.CreateThreadAsync(threadDto);

        if (creado)
        {
            return Ok(new { message = "Hilo creado correctamente." });
        }

        return StatusCode(500, new { message = "Ocurrió un error al crear el hilo." });
    }

    [HttpGet("thread/forum/{forumId}")]
    public async Task<IActionResult> GetThreadsByForum([FromRoute] Guid forumId)
    {
        var threads = await _forumService.GetThreadsByForumIdAsync(forumId);
        return Ok(threads);
    }

    [HttpPost("createMessageInThread")]
    public async Task<IActionResult> CreateMessage([FromBody] CreateForumMessageDTO messageDto)
    {
        if (messageDto == null)
        {
            return BadRequest(new { message = "Los datos del mensaje son requeridos." });
        }

        bool creado = await _forumService.CreateMessageAsync(messageDto);

        if (creado)
        {
            return Ok(new { message = "Mensaje creado correctamente." });
        }

        return StatusCode(500, new { message = "Ocurrió un error al crear el mensaje." });
    }

    [HttpGet("message/thread/{threadId}")]
    public async Task<IActionResult> GetMessagesByThread([FromRoute] Guid threadId)
    {
        var messages = await _forumService.GetMessagesByThreadIdAsync(threadId);
        return Ok(messages);
    }

    [HttpPost("SearchForums")]
    public async Task<IActionResult> SearchForums([FromBody] SearchForumDTO request)
    {
        if (request.Page < 1 || request.Limit < 1)
            return BadRequest("La página y el límite deben ser mayores que 0.");

        try
        {
            var forums = string.IsNullOrWhiteSpace(request.Query)
                ? (await _forumService.GetAllForumsAsync()).ToList()
                : (await _smartSearchService.SearchForumsAsync(request.Query!)).ToList();

            if (forums == null || !forums.Any())
            {
                return NotFound(new { message = "No se han encontrado foros." });

            }

            if (!string.IsNullOrWhiteSpace(request.Country))
            {
                forums = forums
                    .Where(f => !string.IsNullOrWhiteSpace(f.Country)
                                && f.Country.Equals(request.Country, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.SortField))
            {
                var field = request.SortField!.Trim().ToLower();
                var asc = string.Equals(request.SortOrder, "asc", StringComparison.OrdinalIgnoreCase);

                if (field == "title" || field == "titulo")
                {
                    forums = asc
                        ? forums.OrderBy(f => f.Title).ToList()
                        : forums.OrderByDescending(f => f.Title).ToList();
                }
                else if (field == "date" || field == "createdat" || field == "fecha")
                {
                    forums = asc
                        ? forums.OrderBy(f => f.CreatedAt).ToList()
                        : forums.OrderByDescending(f => f.CreatedAt).ToList();
                }
            }

            int totalItems = forums.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.Limit);
            var paged = forums
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .ToList();

            var result = new
            {
                currentPage = request.Page,
                totalPages,
                totalItems,
                items = paged
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error interno del servidor: " + ex.Message);
        }
    }
}
