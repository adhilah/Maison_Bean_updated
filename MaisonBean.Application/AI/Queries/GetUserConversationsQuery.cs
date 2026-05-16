using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Queries;

public class GetUserConversationsQuery
    : IRequest<List<ConversationDto>>
{
    public string UserId { get; set; } = string.Empty;
}

public class GetUserConversationsHandler
    : IRequestHandler<
        GetUserConversationsQuery,
        List<ConversationDto>>
{
    private readonly IConversationService
        _conversationService;

    public GetUserConversationsHandler(
        IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    public async Task<List<ConversationDto>> Handle(
        GetUserConversationsQuery request,
        CancellationToken ct)
    {
        return await _conversationService
            .GetUserConversationsAsync(
                request.UserId,
                ct);
    }
}