// =====================================
// FILE: DeleteConversationCommand.cs
// =====================================

using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Commands;

public class DeleteConversationCommand : IRequest<Unit>
{
    public int ConversationId { get; set; }

    public string UserId { get; set; } = string.Empty;
}

public class DeleteConversationHandler
    : IRequestHandler<DeleteConversationCommand, Unit>
{
    private readonly IConversationService _conversationService;

    public DeleteConversationHandler(
        IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    public async Task<Unit> Handle(
        DeleteConversationCommand request,
        CancellationToken ct)
    {
        if (request.ConversationId <= 0)
        {
            throw new Exception("Invalid conversation id");
        }

        await _conversationService.DeleteConversationAsync(
            request.ConversationId,
            request.UserId,
            ct);

        return Unit.Value;
    }
}