
namespace SimplAgent.AI;

public interface IAgent
{
    string Name { get; }
    IAsyncEnumerable<string> HandleAsync(string input);
}
