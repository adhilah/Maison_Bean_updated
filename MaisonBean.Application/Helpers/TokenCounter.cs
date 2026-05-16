// ====================================
// FILE: TokenCounter.cs
// ====================================

namespace MaisonBean.Application.AI.Helpers;

public static class TokenCounter
{
    // Simple estimation:
    // 1 token ≈ 4 characters

    public static int EstimateTokens(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        return text.Length / 4;
    }

    public static bool ExceedsLimit(
        string text,
        int maxTokens)
    {
        var estimated = EstimateTokens(text);

        return estimated > maxTokens;
    }
}