using SimplAgent.Shared.Dtos.Contracts;

namespace SimplAgent.Web.Web;

public class SimpleAgentClient(HttpClient httpClient)
{
    public async Task<IAsyncEnumerable<PromptResponse>> ChatAsync(PromptRequest promptRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/agents/chat", promptRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response.Content.ReadFromJsonAsAsyncEnumerable<PromptResponse>()!;
    }
}

