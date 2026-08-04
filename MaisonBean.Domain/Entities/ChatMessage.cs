using MaisonBean.Domain.Common;
using MaisonBean.Domain.Enums;

namespace MaisonBean.Domain.Entities;

public class ChatMessage : BaseEntity
{
    public int ChatConversationId { get; set; }

    public ChatConversation ChatConversation { get; set; }
        = null!;

    public MessageRole Role { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}