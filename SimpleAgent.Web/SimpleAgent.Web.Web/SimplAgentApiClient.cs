namespace SimpleAgent.Web.Web;

public class SimplAgentApiClient(HttpClient httpClient)
{
    public async Task<string?> ChatAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync<ChatMessage>("/chat", new ChatMessage(prompt), cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>(cancellationToken: cancellationToken);
    }
}

public record ChatMessage(string Prompt);
