using Microsoft.Agents.AI;

namespace SimplAgent.Core.Contracts
{
    public interface IAgentFactory
    {
        AIAgent CreateAIAgent();
    }

}