using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Queries;

public class GetConversationHistoryQuery
    : IRequest<List<ChatMessageDto>>
{
    public int ConversationId { get; set; }

    public string UserId { get; set; } = string.Empty;
}

public class GetConversationHistoryHandler
    : IRequestHandler<
        GetConversationHistoryQuery,
        List<ChatMessageDto>>
{
    private readonly IConversationService _conversationService;

    public GetConversationHistoryHandler(
        IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    public async Task<List<ChatMessageDto>> Handle(
        GetConversationHistoryQuery request,
        CancellationToken ct)
    {
        if (request.ConversationId <= 0)
        {
            throw new Exception("Invalid conversation id");
        }

        return await _conversationService
            .GetConversationHistoryAsync(
                request.ConversationId,
                request.UserId,
                ct);
    }
}