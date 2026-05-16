// ====================================
// FILE: OpenAIEmbeddingService.cs
// ====================================

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MaisonBean.Application.AI.Interfaces;
using Microsoft.Extensions.Options;

namespace MaisonBean.Infrastructure.AI.OpenAI;

public class OpenAIEmbeddingService
    : IEmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAIOptions _options;

    public OpenAIEmbeddingService(
        HttpClient httpClient,
        IOptions<OpenAIOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _options.ApiKey);
    }

    public async Task<float[]> GenerateEmbeddingAsync(
        string text,
        CancellationToken ct)
    {
        var requestBody = new
        {
            input = text,
            model = _options.EmbeddingModel
        };

        var json = JsonSerializer.Serialize(requestBody);

        var response = await _httpClient.PostAsync(
            "https://api.openai.com/v1/embeddings",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"),
            ct);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content
            .ReadAsStringAsync(ct);

        using var document = JsonDocument.Parse(responseJson);

        var embeddingArray = document
            .RootElement
            .GetProperty("data")[0]
            .GetProperty("embedding");

        return embeddingArray
            .EnumerateArray()
            .Select(x => x.GetSingle())
            .ToArray();
    }
}