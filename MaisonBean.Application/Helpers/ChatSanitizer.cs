// ====================================
// FILE: ChatSanitizer.cs
// ====================================

using System.Text.RegularExpressions;

namespace MaisonBean.Application.AI.Helpers;

public static class ChatSanitizer
{
    private static readonly string[] BlockedPhrases =
    [
        "ignore previous instructions",
        "reveal system prompt",
        "developer mode",
        "bypass security",
        "jailbreak",
        "act as system",
        "disable restrictions"
    ];

    public static string Sanitize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var sanitized = input.Trim();

        sanitized = RemoveHtml(sanitized);

        sanitized = NormalizeWhitespace(sanitized);

        return sanitized;
    }

    public static bool ContainsBlockedContent(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var lowerInput = input.ToLower();

        return BlockedPhrases.Any(
            phrase => lowerInput.Contains(phrase));
    }

    private static string RemoveHtml(string input)
    {
        return Regex.Replace(
            input,
            "<.*?>",
            string.Empty);
    }

    private static string NormalizeWhitespace(string input)
    {
        return Regex.Replace(
            input,
            @"\s+",
            " ");
    }
}