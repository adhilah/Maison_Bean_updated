// ====================================
// FILE: PromptBuilderService.cs
// ====================================

using MaisonBean.Application.AI.Interfaces;
using MaisonBean.Application.AI.Prompts;

namespace MaisonBean.Infrastructure.AI.Prompting;

public class PromptBuilderService : IPromptService
{
    public string BuildChatPrompt(
        string userMessage,
        string context)
    {
        return $@"
Context:
{context}

User Question:
{userMessage}

Answer professionally using the provided context only.
";
    }

    public string BuildRecommendationPrompt(
        string userPreference,
        string products)
    {
        return RecommendationPrompt.Build(
            userPreference,
            products);
    }

    public string BuildSystemPrompt()
    {
        return CoffeeAssistantPrompt.SystemPrompt;
    }
}