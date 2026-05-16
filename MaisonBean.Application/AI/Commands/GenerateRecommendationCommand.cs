// ==========================================
// FILE: GenerateRecommendationCommand.cs
// ==========================================

using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Commands;

public class GenerateRecommendationCommand
    : IRequest<List<RecommendationDto>>
{
    public string Prompt { get; set; } = string.Empty;
}

public class GenerateRecommendationHandler
    : IRequestHandler<
        GenerateRecommendationCommand,
        List<RecommendationDto>>
{
    private readonly IAIChatService _chatService;

    public GenerateRecommendationHandler(
        IAIChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<List<RecommendationDto>> Handle(
        GenerateRecommendationCommand request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            throw new Exception("Prompt is required");
        }

        return await _chatService.GenerateRecommendationsAsync(
            request.Prompt,
            ct);
    }
}