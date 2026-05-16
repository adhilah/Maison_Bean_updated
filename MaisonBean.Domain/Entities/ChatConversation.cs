// ====================================
// FILE: ChatConversation.cs
// ====================================

using MaisonBean.Domain.Common;

namespace MaisonBean.Domain.Entities;

public class ChatConversation : BaseEntity
{
    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ChatMessage> Messages { get; set; }
        = new List<ChatMessage>();
}