using SimplAgent.AI;
using System.Collections.Concurrent;

namespace AgentFramework.Core;

public class AgentRuntime
{
    private readonly ConcurrentDictionary<string, IAgent> _agents = new();

    public void RegisterAgent(IAgent agent)
    {
        if (agent is null) throw new ArgumentNullException(nameof(agent));
        _agents[agent.Name] = agent;
    }

    public async IAsyncEnumerable<string> RouteAsync(string input)
    {
        if (_agents.IsEmpty)
            throw new InvalidOperationException("No agents registered.");
        var agent = _agents.Values.First();
        await foreach (var item in agent.HandleAsync(input))
        {
            yield return item;
        }
    }

    public IReadOnlyCollection<string> RegisteredAgentNames() => _agents.Keys.ToList();
}
