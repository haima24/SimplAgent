namespace SimplAgent.Core.Contracts
{
    public interface IAgentRuntime
    {
        void RegisterAgent(IAgent agent);
        IReadOnlyCollection<string> RegisteredAgentNames();
        IAsyncEnumerable<string> RouteAsync(string input);
    }
}