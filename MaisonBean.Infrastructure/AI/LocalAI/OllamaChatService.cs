using System.Text;
using System.Text.Json;

using MaisonBean.Application.AI.DTOs;
using MaisonBean.Application.AI.Interfaces;

using MaisonBean.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace MaisonBean.Infrastructure.AI.LocalAI;

public class OllamaChatService
    : IAIChatService
{
    private readonly HttpClient _httpClient;

    private readonly AppDbContext _context;

    public OllamaChatService(
        HttpClient httpClient,
        AppDbContext context)
    {
        _httpClient = httpClient;

        _context = context;
    }

    // =====================================
    // ASK AI
    // =====================================

    public async Task<ChatResponseDto>
        AskAsync(
        string message,
        string? conversationId,
        string? userId,
        CancellationToken ct)
    {
        // =====================================
        // GET PRODUCTS FROM DATABASE
        // =====================================

        var products = await _context.Products
            .AsNoTracking()
            .Take(8)
            .Select(x => new
            {
                x.Name,
                x.Description,
                x.Price
            })
            .ToListAsync(ct);

        // =====================================
        // BUILD PRODUCT CONTEXT
        // =====================================

        var productContext =
            string.Join(
                "\n",

                products.Select(p =>
                    $"{p.Name} - {p.Description} - ₹{p.Price}")
            );

        // =====================================
        // AI PROMPT
        // =====================================

        var prompt = $@"
You are an experienced waiter at Maison Bean,
a premium luxury café.

You help customers choose products naturally.

Available products:
{productContext}

Rules:
- Reply in only 2 or 3 short lines
- Keep replies concise
- Recommend products naturally
- Do not use emojis
- Do not use symbols like checkmarks or ticks
- Speak like a real luxury coffee shop waiter

Customer request:
{message}
";

        // =====================================
        // OLLAMA REQUEST
        // =====================================

        var requestBody = new
        {
            model = "tinyllama",

            prompt = prompt,

            stream = false,

            options = new
            {
                num_predict = 40,
                temperature = 0.4,
                top_k = 20,
                top_p = 0.8
            }
        };

        var json =
            JsonSerializer.Serialize(
                requestBody);

        var response =
            await _httpClient.PostAsync(
                "http://localhost:11434/api/generate",

                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"),

                ct);

        response.EnsureSuccessStatusCode();

        // =====================================
        // READ RESPONSE
        // =====================================

        var responseJson =
            await response.Content
                .ReadAsStringAsync(ct);

        using var document =
            JsonDocument.Parse(responseJson);

        var aiResponse =
            document.RootElement
                .GetProperty("response")
                .GetString();

        // =====================================
        // RETURN DTO
        // =====================================

        return new ChatResponseDto
        {
            Response =
                aiResponse ?? "",

            ConversationId =
                conversationId
                ?? Guid.NewGuid()
                    .ToString(),

            Sources =
            [
                "Maison Bean AI"
            ],

            CreatedAt =
                DateTime.UtcNow
        };
    }

    // =====================================
    // RECOMMENDATIONS
    // =====================================

    public async Task<List<RecommendationDto>>
        GenerateRecommendationsAsync(
        string prompt,
        CancellationToken ct)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Take(5)
            .ToListAsync(ct);

        return products.Select(x =>
            new RecommendationDto
            {
                ProductId = x.Id,

                ProductName = x.Name,

                Description =
                    x.Description,

                Price = x.Price,

                ImageUrl = x.Image,

                Reason =
                    "Recommended based on customer interest"
            })
            .ToList();
    }

    // =====================================
    // SUGGESTIONS
    // =====================================

    public async Task<List<string>>
        GetSuggestionsAsync(
        string prompt,
        CancellationToken ct)
    {
        await Task.CompletedTask;

        return
        [
            "Best strong coffee",
            "Cold brew options",
            "Coffee for studying",
            "Low sugar coffee",
            "Best selling coffee",
            "Coffee with almond milk"
        ];
    }
}