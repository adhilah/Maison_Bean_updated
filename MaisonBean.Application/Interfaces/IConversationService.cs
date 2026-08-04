using MaisonBean.Application.AI.DTOs;

namespace MaisonBean.Application.AI.Interfaces;

public interface IConversationService
{
    Task<int> CreateConversationAsync(
        string userId,
        string title,
        CancellationToken ct);

    Task DeleteConversationAsync(
        int conversationId,
        string userId,
        CancellationToken ct);

    Task<List<ChatMessageDto>> GetConversationHistoryAsync(
        int conversationId,
        string userId,
        CancellationToken ct);

    Task SaveMessageAsync(
        int conversationId,
        string role,
        string message,
        CancellationToken ct);

    Task<List<ConversationDto>> GetUserConversationsAsync(
        string userId,
        CancellationToken ct);
}