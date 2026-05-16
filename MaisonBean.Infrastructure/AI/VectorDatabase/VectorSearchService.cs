// ====================================
// FILE: VectorSearchService.cs
// ====================================

using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;

namespace MaisonBean.Infrastructure.AI.VectorDatabase;

public class VectorSearchService
    : IVectorSearchService
{
    public async Task<List<SearchResultDto>>
        SearchAsync(
        string query,
        CancellationToken ct)
    {
        await Task.CompletedTask;

        // Temporary mock implementation

        return
        [
            new SearchResultDto
            {
                Title = "Dark Roast Coffee",
                Content =
                    "Strong bold coffee with rich flavor",
                Source = "Products",
                Score = 0.95
            }
        ];
    }

    public async Task StoreEmbeddingAsync(
        string id,
        string content,
        float[] embedding,
        CancellationToken ct)
    {
        await Task.CompletedTask;
    }

    public async Task DeleteEmbeddingAsync(
        string id,
        CancellationToken ct)
    {
        await Task.CompletedTask;
    }
}