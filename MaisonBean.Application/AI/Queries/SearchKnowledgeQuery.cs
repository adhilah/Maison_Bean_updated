// ======================================
// FILE: SearchKnowledgeQuery.cs
// ======================================

using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.AI.Queries;

public class SearchKnowledgeQuery
    : IRequest<List<SearchResultDto>>
{
    public string Query { get; set; } = string.Empty;
}

public class SearchKnowledgeHandler
    : IRequestHandler<
        SearchKnowledgeQuery,
        List<SearchResultDto>>
{
    private readonly IVectorSearchService _vectorSearchService;

    public SearchKnowledgeHandler(
        IVectorSearchService vectorSearchService)
    {
        _vectorSearchService = vectorSearchService;
    }

    public async Task<List<SearchResultDto>> Handle(
        SearchKnowledgeQuery request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
        {
            throw new Exception("Query is required");
        }

        return await _vectorSearchService.SearchAsync(
            request.Query,
            ct);
    }
}