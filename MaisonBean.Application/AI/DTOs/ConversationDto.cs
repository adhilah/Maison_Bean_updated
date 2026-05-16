// ===========================
// FILE: ConversationDto.cs
// ===========================

namespace MaisonBean.Application.AI.DTOs;

public class ConversationDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<ChatMessageDto> Messages { get; set; } = [];
}