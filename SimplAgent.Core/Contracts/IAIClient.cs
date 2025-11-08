using Microsoft.Agents.AI;
using SimplAgent.Shared.Dtos.Configuration;

namespace SimplAgent.Core.Contracts;

public interface IAIClient
{
    AIAgent CreateAzureOpenAIAgentAsync(AzureOpenAIConfig config);
}
