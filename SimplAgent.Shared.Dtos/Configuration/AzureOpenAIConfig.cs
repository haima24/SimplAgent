namespace SimplAgent.Shared.Dtos.Configuration;

public class AzureOpenAIConfig
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public string Instructions { get; set; } = "You are a helpful AI assistant.";
    public string AgentName { get; set; } = "SimplAgent";
    public string ModelName { get; set; } = "gpt-4o-mini";
}
