using MaisonBean.Application.AI.DTOs;

namespace MaisonBean.Application.AI.Interfaces;

public interface IVectorSearchService
{
    Task<List<SearchResultDto>> SearchAsync(
        string query,
        CancellationToken ct);

    Task StoreEmbeddingAsync(
        string id,
        string content,
        float[] embedding,
        CancellationToken ct);

    Task DeleteEmbeddingAsync(
        string id,
        CancellationToken ct);
}