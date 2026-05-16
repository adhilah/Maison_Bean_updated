// ====================================
// FILE: KnowledgeDocument.cs
// ====================================

using MaisonBean.Domain.Common;
using MaisonBean.Domain.Enums;

namespace MaisonBean.Domain.Entities;

public class KnowledgeDocument : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public KnowledgeType Type { get; set; }

    public string Source { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public ICollection<EmbeddingRecord> Embeddings { get; set; }
        = new List<EmbeddingRecord>();
}