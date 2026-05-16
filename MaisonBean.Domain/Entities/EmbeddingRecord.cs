// ====================================
// FILE: EmbeddingRecord.cs
// ====================================

using MaisonBean.Domain.Common;

namespace MaisonBean.Domain.Entities;

public class EmbeddingRecord : BaseEntity
{
    public int KnowledgeDocumentId { get; set; }

    public KnowledgeDocument KnowledgeDocument { get; set; }
        = null!;

    // Serialized vector
    public string Vector { get; set; } = string.Empty;

    public string Metadata { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}