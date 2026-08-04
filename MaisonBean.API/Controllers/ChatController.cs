using MaisonBean.Application.AI.Commands;
using MaisonBean.Application.AI.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Text;

namespace MaisonBean.API.Controllers;

[ApiController]
[Authorize(Roles = "CUSTOMER")]
//[EnableRateLimiting("ai")]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // ASK AI
    // =========================================
    //[HttpPost]
    //public async Task<IActionResult> AskAI(
    //    AskAICommand command)
    //{
    //    command.UserId = GetUserId();

    //    var response = await _mediator.Send(command);

    //    return Ok(response);
    //}


    [HttpPost]
    public async Task<IActionResult> AskAI(
    [FromBody] AskAICommand command)
    {
        try
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message,
                stack = ex.StackTrace
            });
        }
    }
    // AI RECOMMENDATIONS
    [HttpPost("recommendations")]
    public async Task<IActionResult> GenerateRecommendations(
        GenerateRecommendationCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    // CREATE CONVERSATION
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

    // DELETE CONVERSATION
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

    // GET CHAT HISTORY
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

    // GET USER CONVERSATIONS
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

    // SEARCH KNOWLEDGE
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

    // GET AI SUGGESTIONS
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

    // HELPER
    private string GetUserId()
    {
        return User.FindFirstValue("id")
            ?? throw new Exception("User not found");
    }
}
