using Microsoft.Extensions.AI;
using Microsoft.KernelMemory;
using SimplAgent.Shared.Dtos.Contracts;
using System.ComponentModel;

namespace SimplAgent.AITools.SimpleTools;

public sealed class KnowledgeTool
{
    private readonly IKnowledgeService _knowledgeService;

    public KnowledgeTool(IKnowledgeService knowledgeService)
    {
        _knowledgeService = knowledgeService ?? throw new ArgumentNullException(nameof(knowledgeService));
    }

    [Description("Search knowledge base")]
    public async Task<SearchResult> SearchAsync([Description("the value to search")] string query)
    {
        var result = await _knowledgeService.SearchAsync(query);
        return result;
    }

    public AITool AsAITool()
    {
        return AIFunctionFactory.Create(this.SearchAsync, new AIFunctionFactoryOptions { Name = "memory" });
    }
}
