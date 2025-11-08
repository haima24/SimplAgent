namespace SimplAgent.Core.Contracts;

public interface IAgent
{
    string Name { get; }
    IAsyncEnumerable<string> HandleAsync(string input);
}
