namespace SimplAgent.Shared.Dtos.Contracts;

public class DocumentDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? KnowledgeDocId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
