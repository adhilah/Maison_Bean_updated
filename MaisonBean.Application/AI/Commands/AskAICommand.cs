using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace MaisonBean.Application.AI.Commands;

public class AskAICommand : IRequest<ChatResponseDto>
{
    public string Message { get; set; } = string.Empty;

    public string? ConversationId { get; set; }

    [JsonIgnore]
    public string? UserId { get; set; }
}

public class AskAIHandler
    : IRequestHandler<AskAICommand, ChatResponseDto>
{
    private readonly IAIChatService _chatService;

    public AskAIHandler(
        IAIChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<ChatResponseDto> Handle(
        AskAICommand request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new Exception("Message is required");
        }

        return await _chatService.AskAsync(
            request.Message,
            request.ConversationId,
            request.UserId,
            ct);
    }
}