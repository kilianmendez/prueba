using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Models.Database.Entities;
using Backend.Services;
using Backend.Models.Interfaces;
using Backend.Models.Dtos;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }


    [HttpGet("{userId}")]
    public async Task<ActionResult<List<ChatDTO>>> GetChats(Guid userId)
    {
        var chats = await _chatService.ObtenerChatsDeUsuarioAsync(userId);
        return chats.Any()
            ? Ok(chats)
            : NotFound(new { message = "Aún no tienes conversaciones. Solo puedes chatear con usuarios que te siguen y a los que sigues." });
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<MessageDTO>>> GetChatHistory(
            [FromQuery] Guid userId,
            [FromQuery] Guid contactId)
    {
        if (userId == Guid.Empty || contactId == Guid.Empty)
            return BadRequest(new { message = "Debe indicar userId y contactId válidos." });

        var history = await _chatService.GetChatHistoryAsync(userId, contactId);
        if (history == null || history.Count == 0)
            return NotFound(new { message = "No hay mensajes entre estos usuarios." });

        return Ok(history);
    }
}

