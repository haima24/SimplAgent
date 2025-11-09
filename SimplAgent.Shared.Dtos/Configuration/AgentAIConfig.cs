namespace SimplAgent.Shared.Dtos.Configuration;

public class AgentAIConfig
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string Instructions { get; set; } = "You are a helpful AI assistant.";
    public string AgentName { get; set; } = "SimplAgent";
    public string Model { get; set; } = "gpt-5-mini";
}
