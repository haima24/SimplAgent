using Microsoft.Agents.AI;
using Microsoft.Extensions.Options;

namespace SimplAgent.AI;

public class AssistantAgent : IAgent
{

    public string Name => "assistant";
    private readonly IAIClient _aiClient;
    private readonly AzureOpenAIConfig _azureOpenAIConfig;
    public AssistantAgent(IAIClient aiClient,
        IOptions<AzureOpenAIConfig> options
        )
    {
        _aiClient = aiClient;
        _azureOpenAIConfig = options.Value;
    }
    public async IAsyncEnumerable<string> HandleAsync(string input)
    {
        var prompt = $"""You are a helpful assistant in a Microsoft Teams bot. User: {input}""";
        var agent = _aiClient.CreateAzureOpenAIAgentAsync(_azureOpenAIConfig);
        await foreach (var item in agent.RunStreamingAsync(prompt, options: new ChatClientAgentRunOptions()))
            yield return item.Text;
    }
}
