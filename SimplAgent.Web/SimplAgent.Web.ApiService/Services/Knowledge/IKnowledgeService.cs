using SimplAgent.Shared.Dtos.Contracts;

namespace NTG.Agent.Orchestrator.Services.Knowledge;

public interface IKnowledgeService
{
    Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(CancellationToken cancellationToken = default);
    Task<DocumentDto> ImportDocumentAsync(Stream content, string fileName, CancellationToken cancellationToken = default);
    Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
}
