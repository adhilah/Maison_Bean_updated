// ===========================
// FILE: ChatResponseDto.cs
// ===========================

namespace MaisonBean.Application.AI.DTOs;

public class ChatResponseDto
{
    public string Response { get; set; } = string.Empty;

    public string? ConversationId { get; set; }

    public List<string> Sources { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}