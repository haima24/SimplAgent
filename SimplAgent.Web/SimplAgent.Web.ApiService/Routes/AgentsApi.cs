using Microsoft.Agents.AI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Diagnostics.Latency;
using SimplAgent.Shared.Dtos.Contracts;
using SimplAgent.Web.ApiService.Services.AgentFactory;

namespace SimplAgent.Web.ApiService.Routes;

public static class AgentsApi
{
    public static void MapAgentsApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/agents")
            .WithOpenApi()
            .WithTags("Agents");

        // POST /api/agents/chat
        group.MapPost("/chat", ChatAsync)
            .WithName("ChatWithAgent")
            .WithDescription("Send a chat prompt to the AI agent and stream responses.")
            .Produces<PromptResponse>();
    }

    private static async IAsyncEnumerable<PromptResponse> ChatAsync(
        [FromBody] PromptRequest promptRequest,
        [FromServices] IAgentFactory agentFactory)
    {
        var agent = agentFactory.CreateAIAgent();

        var tools = await agentFactory.GetAvailableTools();

        var prompt = agentFactory.BuildTextOnlyPrompt(promptRequest.Prompt);

        var chatOptions = new ChatOptions
        {
            Tools = tools
        };
        await foreach (var response in agent.RunStreamingAsync(prompt, options: new ChatClientAgentRunOptions(chatOptions)))
        {
            yield return new PromptResponse(response.Text);
        }
    }
}
