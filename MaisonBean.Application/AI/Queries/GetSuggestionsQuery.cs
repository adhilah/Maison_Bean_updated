using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Queries;

public class GetSuggestionsQuery
    : IRequest<List<string>>
{
    public string Prompt { get; set; } = string.Empty;
}

public class GetSuggestionsHandler
    : IRequestHandler<
        GetSuggestionsQuery,
        List<string>>
{
    private readonly IAIChatService _chatService;

    public GetSuggestionsHandler(
        IAIChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<List<string>> Handle(
        GetSuggestionsQuery request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            return [];
        }

        return await _chatService.GetSuggestionsAsync(
            request.Prompt,
            ct);
    }
}