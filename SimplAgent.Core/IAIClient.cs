using Microsoft.Agents.AI;

namespace SimplAgent.AI
{
    public interface IAIClient
    {
        AIAgent CreateAzureOpenAIAgentAsync(AzureOpenAIConfig config);
    }
}