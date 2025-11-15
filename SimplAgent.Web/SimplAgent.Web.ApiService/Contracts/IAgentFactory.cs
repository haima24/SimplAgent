using Microsoft.Agents.AI;

namespace SimplAgent.Web.ApiService.Contracts
{
    public interface IAgentFactory
    {
        AIAgent CreateAIAgent();
    }

}