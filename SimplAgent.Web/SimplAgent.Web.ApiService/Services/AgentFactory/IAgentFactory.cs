using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace SimplAgent.Web.ApiService.Services.AgentFactory;

public interface IAgentFactory
{
    string BuildTextOnlyPrompt(string userPrompt);
    AIAgent CreateAIAgent();
    Task<List<AITool>> GetAvailableTools();
}