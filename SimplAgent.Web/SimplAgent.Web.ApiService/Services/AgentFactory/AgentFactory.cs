using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using SimplAgent.Web.ApiService.Configuration;
using System.ClientModel;

namespace SimplAgent.Web.ApiService.Services.AgentFactory;

public class AgentFactory : IAgentFactory
{
    private readonly AgentAIConfig _config;

    public AgentFactory(IOptions<AgentAIConfig> configOptions)
    {
        _config = configOptions.Value;
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
