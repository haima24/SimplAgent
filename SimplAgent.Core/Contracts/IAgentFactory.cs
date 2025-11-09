using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using SimplAgent.Shared.Dtos.Configuration;

namespace SimplAgent.Core.Contracts
{
    public interface IAgentFactory
    {
        AIAgent CreateAIAgent();
    }

}