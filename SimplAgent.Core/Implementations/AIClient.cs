using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using SimplAgent.Core.Contracts;
using SimplAgent.Shared.Dtos.Configuration;
using System.ClientModel;
namespace SimplAgent.Core.Implementations;

public class AIClient : IAIClient
{
    public AIAgent CreateAzureOpenAIAgentAsync(AzureOpenAIConfig config)
    {
        var chatClient = new AzureOpenAIClient(
            new Uri(config.Endpoint),
            new ApiKeyCredential(config.ApiKey))
             .GetChatClient(config.ModelName)
             .AsIChatClient()
             .AsBuilder()
             .UseFunctionInvocation()
             .Build();


        return Create(chatClient, instructions: config.Instructions, name: "SimplAgent", tools: new List<AITool>());
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
