using MaisonBean.Application.AI.Commands;
using MaisonBean.Application.AI.Queries;
using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MaisonBean.API.Controllers;

[ApiController]
[Authorize(Roles = "CUSTOMER")]
[EnableRateLimiting("ai")]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // =========================================
    // ASK AI
    // POST: api/chat
    // =========================================

    [HttpPost]
    public async Task<IActionResult> AskAI(
        AskAICommand command)
    {
        command.UserId = GetUserId();

        var response = await _mediator.Send(command);

        return Ok(response);
    }

    // =========================================
    // AI RECOMMENDATIONS
    // POST: api/chat/recommendations
    // =========================================

    [HttpPost("recommendations")]
    public async Task<IActionResult> GenerateRecommendations(
        GenerateRecommendationCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    // =========================================
    // CREATE CONVERSATION
    // POST: api/chat/conversation
    // =========================================

    [HttpPost("conversation")]
    public async Task<IActionResult> CreateConversation(
        SaveConversationCommand command)
    {
        command.UserId = GetUserId();

        var conversationId =
            await _mediator.Send(command);

        return Ok(new
        {
            ConversationId = conversationId
        });
    }

    // =========================================
    // DELETE CONVERSATION
    // DELETE: api/chat/conversation/{id}
    // =========================================

    [HttpDelete("conversation/{id}")]
    public async Task<IActionResult> DeleteConversation(
        int id)
    {
        var command = new DeleteConversationCommand
        {
            ConversationId = id,
            UserId = GetUserId()
        };

        await _mediator.Send(command);

        return Ok(new
        {
            message =
                "Conversation deleted successfully"
        });
    }

    // =========================================
    // GET CHAT HISTORY
    // GET: api/chat/conversation/{id}
    // =========================================

    [HttpGet("conversation/{id}")]
    public async Task<IActionResult> GetConversationHistory(
        int id)
    {
        var query = new GetConversationHistoryQuery
        {
            ConversationId = id,
            UserId = GetUserId()
        };

        var response = await _mediator.Send(query);

        return Ok(response);
    }

    // =========================================
    // GET USER CONVERSATIONS
    // GET: api/chat/conversations
    // =========================================

    [HttpGet("conversations")]
    public async Task<IActionResult> GetUserConversations()
    {
        var query = new GetUserConversationsQuery
        {
            UserId = GetUserId()
        };

        var response = await _mediator.Send(query);

        return Ok(response);
    }

    // =========================================
    // SEARCH KNOWLEDGE
    // GET: api/chat/search?query=
    // =========================================

    [HttpGet("search")]
    public async Task<IActionResult> SearchKnowledge(
        [FromQuery] string query)
    {
        var searchQuery =
            new SearchKnowledgeQuery
            {
                Query = query
            };

        var response =
            await _mediator.Send(searchQuery);

        return Ok(response);
    }

    // =========================================
    // GET AI SUGGESTIONS
    // GET: api/chat/suggestions?prompt=
    // =========================================

    [HttpGet("suggestions")]
    public async Task<IActionResult> GetSuggestions(
        [FromQuery] string prompt)
    {
        var query = new GetSuggestionsQuery
        {
            Prompt = prompt
        };

        var response = await _mediator.Send(query);

        return Ok(response);
    }

    // =========================================
    // HELPER
    // =========================================

    private string GetUserId()
    {
        return User.FindFirstValue("id")
            ?? throw new Exception("User not found");
    }
}
