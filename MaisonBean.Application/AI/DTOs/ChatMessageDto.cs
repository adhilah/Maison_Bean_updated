namespace MaisonBean.Application.AI.DTOs;

public class ChatMessageDto
{
    public string Role { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}