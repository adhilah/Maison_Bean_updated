using MaisonBean.Application.AI.DTOs;
using System.Text;

namespace MaisonBean.Application.AI.Helpers;

public static class ContextFormatter
{
    public static string FormatSearchResults(
        List<SearchResultDto> results)
    {
        if (results == null || results.Count == 0)
        {
            return "No relevant context found.";
        }

        var builder = new StringBuilder();

        foreach (var item in results)
        {
            builder.AppendLine($"Title: {item.Title}");
            builder.AppendLine($"Content: {item.Content}");
            builder.AppendLine($"Source: {item.Source}");
            builder.AppendLine();
        }

        return builder.ToString();
    }

    public static string FormatConversation(
        List<ChatMessageDto> messages)
    {
        if (messages == null || messages.Count == 0)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();

        foreach (var message in messages)
        {
            builder.AppendLine(
                $"{message.Role}: {message.Message}");
        }

        return builder.ToString();
    }
}