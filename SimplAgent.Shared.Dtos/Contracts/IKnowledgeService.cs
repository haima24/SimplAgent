using Microsoft.KernelMemory;

namespace SimplAgent.Shared.Dtos.Contracts;

public interface IKnowledgeService
{
    Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(CancellationToken cancellationToken = default);
    Task<DocumentDto> ImportDocumentAsync(Stream content, string fileName, CancellationToken cancellationToken = default);
    Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task<SearchResult> SearchAsync(string query, CancellationToken cancellationToken = default);
}
