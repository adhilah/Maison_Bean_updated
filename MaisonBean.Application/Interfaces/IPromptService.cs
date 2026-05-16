// ============================
// FILE: IPromptService.cs
// ============================

namespace MaisonBean.Application.AI.Interfaces;

public interface IPromptService
{
    string BuildChatPrompt(
        string userMessage,
        string context);

    string BuildRecommendationPrompt(
        string userPreference,
        string products);

    string BuildSystemPrompt();
}