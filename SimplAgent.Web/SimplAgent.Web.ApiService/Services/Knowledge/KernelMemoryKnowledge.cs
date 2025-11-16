using Microsoft.KernelMemory;

namespace NTG.Agent.Orchestrator.Services.Knowledge;

public class KernelMemoryKnowledge : IKnowledgeService
{
    private readonly IKernelMemory _kernelMemory;

    private readonly ILogger<KernelMemoryKnowledge> _logger;

    public KernelMemoryKnowledge(IKernelMemory kernelMemory, ILogger<KernelMemoryKnowledge> logger)
    {
        _kernelMemory = kernelMemory ?? throw new ArgumentNullException(nameof(kernelMemory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<string> ImportDocumentAsync(Stream content, string fileName, CancellationToken cancellationToken = default)
    {
        return await _kernelMemory.ImportDocumentAsync(content, fileName);
    }

    public async Task RemoveDocumentAsync(string documentId, CancellationToken cancellationToken = default)
    {
        await _kernelMemory.DeleteDocumentAsync(documentId);
    }
}
