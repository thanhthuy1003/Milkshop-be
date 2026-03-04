using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET1814_MilkShop.API.CoreHelpers.ActionFilters;
using NET1814_MilkShop.API.CoreHelpers.Extensions;
using NET1814_MilkShop.Repositories.Models.ConversationModels;
using NET1814_MilkShop.Services.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace NET1814_MilkShop.API.Controllers;

[ApiController]
[Route("api/conversations")]
[Authorize(AuthenticationSchemes = "Access", Roles = "3,4")]
[ServiceFilter(typeof(UserExistsFilter))]
public class ConversationController : Controller
{
    private readonly IConversationService _conversationService;
    private readonly ILogger _logger;

    public ConversationController(IConversationService conversationService, ILogger logger)
    {
        _conversationService = conversationService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyConversations([FromQuery] ConversationQueryModel query)
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information("User {userId} get conversations", userId);
        var res = await _conversationService.GetMyConversationsAsync(userId, query);
        return ResponseExtension.Result(res);
    }

    [HttpPost("{otherUserId}")]
    public async Task<IActionResult> GetOrCreateConversation(Guid otherUserId)
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information("User {userId} open conversation with {otherUserId}", userId, otherUserId);
        var res = await _conversationService.GetOrCreateConversationAsync(userId, otherUserId);
        return ResponseExtension.Result(res);
    }

    [HttpGet("{conversationId}/messages")]
    public async Task<IActionResult> GetMessages(Guid conversationId, [FromQuery] ConversationQueryModel query)
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information("User {userId} get messages of conversation {conversationId}", userId, conversationId);
        var res = await _conversationService.GetMessagesAsync(userId, conversationId, query);
        return ResponseExtension.Result(res);
    }

    [HttpPost("{conversationId}/messages")]
    public async Task<IActionResult> SendMessage(Guid conversationId, [FromBody] SendMessageModel model)
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information("User {userId} send message to conversation {conversationId}", userId, conversationId);
        var res = await _conversationService.SendMessageAsync(userId, conversationId, model);
        return ResponseExtension.Result(res);
    }

    [HttpPatch("{conversationId}/read")]
    public async Task<IActionResult> MarkRead(Guid conversationId)
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        _logger.Information("User {userId} mark messages read in conversation {conversationId}", userId, conversationId);
        var res = await _conversationService.MarkMessagesReadAsync(userId, conversationId);
        return ResponseExtension.Result(res);
    }
}
