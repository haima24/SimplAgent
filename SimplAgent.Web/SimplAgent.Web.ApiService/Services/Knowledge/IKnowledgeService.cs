namespace NTG.Agent.Orchestrator.Services.Knowledge;

public interface IKnowledgeService
{
    Task<string> ImportDocumentAsync(Stream content, string fileName, CancellationToken cancellationToken = default);
    Task RemoveDocumentAsync(string documentId, CancellationToken cancellationToken = default);
}
