using Microsoft.Agents.AI;

namespace SimplAgent.Web.ApiService.Services.AgentFactory;

public interface IAgentFactory
{
    AIAgent CreateAIAgent();
}