using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;

namespace MaisonBean.Infrastructure.AI.Conversations;

public class ConversationService
    : IConversationService
{
    public async Task<int> CreateConversationAsync(
        string userId,
        string title,
        CancellationToken ct)
    {
        await Task.CompletedTask;

        return 1;
    }

    public async Task DeleteConversationAsync(
        int conversationId,
        string userId,
        CancellationToken ct)
    {
        await Task.CompletedTask;
    }

    public async Task<List<ChatMessageDto>>
        GetConversationHistoryAsync(
        int conversationId,
        string userId,
        CancellationToken ct)
    {
        await Task.CompletedTask;

        return [];
    }

    public async Task SaveMessageAsync(
        int conversationId,
        string role,
        string message,
        CancellationToken ct)
    {
        await Task.CompletedTask;
    }

    public async Task<List<ConversationDto>>
        GetUserConversationsAsync(
        string userId,
        CancellationToken ct)
    {
        await Task.CompletedTask;

        return [];
    }
}