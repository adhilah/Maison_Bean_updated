using MaisonBean.Application.AI.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Commands;

public class SaveConversationCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

public class SaveConversationHandler
    : IRequestHandler<SaveConversationCommand, int>
{
    private readonly IConversationService _conversationService;

    public SaveConversationHandler(
        IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    public async Task<int> Handle(
        SaveConversationCommand request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            throw new Exception("UserId is required");
        }

        return await _conversationService
            .CreateConversationAsync(
                request.UserId,
                request.Title,
                ct);
    }
}