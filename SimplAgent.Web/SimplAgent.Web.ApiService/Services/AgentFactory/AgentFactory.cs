using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using SimplAgent.AITools.SimpleTools;
using SimplAgent.Shared.Dtos.Contracts;
using SimplAgent.Web.ApiService.Configuration;
using System.ClientModel;

namespace SimplAgent.Web.ApiService.Services.AgentFactory;

public class AgentFactory : IAgentFactory
{
    private readonly AgentAIConfig _config;

    private readonly IKnowledgeService _knowledgeService;

    public AgentFactory(IOptions<AgentAIConfig> configOptions,
        IKnowledgeService knowledgeService
        )
    {
        _config = configOptions.Value;
        _knowledgeService = knowledgeService;
    }

    public AIAgent CreateAIAgent()
    {
        var chatClient = new AzureOpenAIClient(
                    new Uri(_config.Endpoint),
                    new ApiKeyCredential(_config.ApiKey))
                     .GetChatClient(_config.Model)
                     .AsIChatClient()
                     .AsBuilder()
                     .UseFunctionInvocation()
                     .Build();

        return Create(chatClient, instructions: _config.Instructions, name: "SimplAgent", tools: new List<AITool>());
    }

    public async Task<List<AITool>> GetAvailableTools()
    {
        // 1. Define built-in tools (static plugins)
        var allTools = new List<AITool>
        {
            new KnowledgeTool(_knowledgeService).AsAITool()
        };

        //// 2. Add MCP tools (from remote MCP server)
        //if (!string.IsNullOrEmpty(agent.McpServer?.Trim()))
        //{
        //    var mcpTools = await GetMcpToolsAsync(agent.McpServer);
        //    allTools.AddRange(mcpTools);
        //}
        await Task.CompletedTask;
        return allTools;
    }

    public string BuildTextOnlyPrompt(string userPrompt) =>
        $@"
            Question: {userPrompt}. Context: {{memory.search}}
            Given the context and provided history information, tools definitions and prior knowledge, reply to the user question.
            If the answer is not in the context, inform the user that you can't answer the question.
        ";

    private AIAgent Create(IChatClient chatClient, string instructions, string name, List<AITool> tools)
    {
        var agent = new ChatClientAgent(chatClient,
            name: name,
            instructions: instructions,
            tools: tools)
            .AsBuilder()
            .Build();
        return agent;
    }
}
