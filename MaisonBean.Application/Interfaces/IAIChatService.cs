using MaisonBean.Application.AI.DTOs;

namespace MaisonBean.Application.AI.Interfaces;

public interface IAIChatService
{
    // ASK AI
    Task<ChatResponseDto>
        AskAsync(
        string message,
        string? conversationId,
        string? userId,
        CancellationToken ct);

    // RECOMMENDATIONS
    Task<List<RecommendationDto>>
        GenerateRecommendationsAsync(
        string prompt,
        CancellationToken ct);


    // SUGGESTIONS
    Task<List<string>>
        GetSuggestionsAsync(
        string prompt,
        CancellationToken ct);
}