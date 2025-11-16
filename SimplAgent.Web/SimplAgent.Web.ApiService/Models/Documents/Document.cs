namespace SimplAgent.Web.ApiService.Models.Documents;

public class Document
{
    public Guid Id { get; set; }

    public Document()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
