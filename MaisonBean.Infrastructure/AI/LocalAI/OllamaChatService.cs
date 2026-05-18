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

        //var productContext =
        //    string.Join(
        //        "\n",

        //        products.Select(p =>
        //            $"{p.Name} - {p.Description} - ₹{p.Price}")
        //    );

        var productContext =
    string.Join(
        "\n\n",

        products.Select(p =>
            $"""
PRODUCT:
Name: {p.Name}
Description: {p.Description}
Price: ₹{p.Price}
""")
    );

        // =====================================
        // AI PROMPT
        // =====================================

        var prompt = $@"
You are Maison Bean AI,
a premium coffee assistant.

STRICT RULES:

- Answer ONLY using products provided below
- Never invent coffee beans
- Never invent product names
- Never invent origins
- Never hallucinate menu items
- If unavailable, say:
  'Currently unavailable in our menu.'

AVAILABLE PRODUCTS:
{productContext}

CUSTOMER QUESTION:
{message}

RESPONSE RULES:

- Maximum 4 short lines
- Mobile friendly
- No markdown
- No long paragraphs

FORMAT:

Recommendation:
Short answer

Reason:
Short reason
";

        // =====================================
        // OLLAMA REQUEST
        // =====================================

        var requestBody = new
        {
            model = "phi3:latest",

            messages = new[]
     {
        new
        {
            role = "user",
            content = prompt
        }
    },

            stream = false
        };

        var json =
            JsonSerializer.Serialize(
                requestBody);

        var response =
            await _httpClient.PostAsync(
                "http://localhost:11434/api/chat",

                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"),

                ct);

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content
                    .ReadAsStringAsync(ct);

            throw new Exception(
                $"Ollama Error: {error}");
        }

        // =====================================
        // READ RESPONSE
        // =====================================

        var responseJson =
            await response.Content
                .ReadAsStringAsync(ct);

        using var document =
            JsonDocument.Parse(responseJson);

        //var aiResponse =
        //    document.RootElement
        //        .GetProperty("response")
        //        .GetString();
        var aiResponse =
    document.RootElement
        .GetProperty("message")
        .GetProperty("content")
        .GetString();
        var validProducts =
    products.Select(x =>
        x.Name.ToLower())
    .ToList();

        var containsValidProduct =
            validProducts.Any(product =>
                aiResponse!
                    .ToLower()
                    .Contains(product));

        if (!containsValidProduct)
        {
            aiResponse =
                "Currently unavailable in our menu.";
        }

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