using Microsoft.EntityFrameworkCore;
using Microsoft.KernelMemory;
using SimplAgent.Shared.Dtos.Contracts;
using SimplAgent.Shared.Dtos.Services;
using SimplAgent.Web.ApiService.Data;

namespace NTG.Agent.Orchestrator.Services.Knowledge;

public class KernelMemoryKnowledge : IKnowledgeService
{
    private readonly IKernelMemory _kernelMemory;

    private readonly ILogger<KernelMemoryKnowledge> _logger;

    private readonly AgentDbContext _agentDbContext;

    public KernelMemoryKnowledge(IKernelMemory kernelMemory,
        ILogger<KernelMemoryKnowledge> logger,
        AgentDbContext agentDbContext)
    {
        _kernelMemory = kernelMemory ?? throw new ArgumentNullException(nameof(kernelMemory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _agentDbContext = agentDbContext ?? throw new ArgumentNullException(nameof(agentDbContext));
    }

    public async Task<DocumentDto> ImportDocumentAsync(Stream content, string fileName, CancellationToken cancellationToken = default)
    {
        var documentID = await _kernelMemory.ImportDocumentAsync(content, fileName);
        var document = new SimplAgent.Web.ApiService.Models.Documents.Document
        {
            Id = Guid.NewGuid(),
            Name = fileName,
            KnowledgeDocId = documentID,
            CreatedAt = DateTime.UtcNow
        };
        await _agentDbContext.Documents.AddAsync(document, cancellationToken);

        await _agentDbContext.SaveChangesAsync(cancellationToken);

        return document.MapTo<DocumentDto>();
    }

    public async Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var doc = await _agentDbContext.Documents
            .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken);

        if (doc != null)
        {
            if (!string.IsNullOrEmpty(doc.KnowledgeDocId))
            {
                await _kernelMemory.DeleteDocumentAsync(doc.KnowledgeDocId);
            }
            _agentDbContext.Documents.Remove(doc);
            await _agentDbContext.SaveChangesAsync(cancellationToken);
        }

    }

    public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(CancellationToken cancellationToken = default)
    {
        return await _agentDbContext.Documents
            .Select(x => x.MapTo<DocumentDto>())
            .ToListAsync(cancellationToken);
    }
}
