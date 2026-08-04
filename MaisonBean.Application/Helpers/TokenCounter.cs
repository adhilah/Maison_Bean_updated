namespace MaisonBean.Application.AI.Helpers;

public static class TokenCounter
{
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