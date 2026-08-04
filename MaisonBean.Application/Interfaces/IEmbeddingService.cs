namespace MaisonBean.Application.AI.Interfaces;

public interface IEmbeddingService
{
    Task<float[]> GenerateEmbeddingAsync(
        string text,
        CancellationToken ct);
}