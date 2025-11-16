using Microsoft.AspNetCore.Components.Forms;
using SimplAgent.Shared.Dtos.Contracts;
using SimplAgent.Shared.Dtos.Services;

namespace SimplAgent.Web.Web;

public class SimpleAgentClient(HttpClient httpClient)
{
    public async Task<IAsyncEnumerable<PromptResponse>> ChatAsync(PromptRequest promptRequest, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/agents/chat", promptRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response.Content.ReadFromJsonAsAsyncEnumerable<PromptResponse>()!;
    }

    public async Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/knowledge/documents/{documentId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("api/knowledge/documents", cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<DocumentDto>>(cancellationToken: cancellationToken);
        return result ?? Enumerable.Empty<DocumentDto>();
    }

    public async Task<IEnumerable<DocumentDto>> UploadDocumentsAsync(IList<IBrowserFile> files)
    {
        long maxFileSize = 5 * 1024L * 1024L; // 5 MB
        using var content = new MultipartFormDataContent();
        foreach (var file in files)
        {
            if (file.Size > 0)
            {
                var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

                // Get content type from file or fallback to detection by extension
                var contentType = !string.IsNullOrEmpty(file.ContentType)
                    ? file.ContentType
                    : FileTypeService.GetContentType(file.Name);

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                content.Add(fileContent, "files", file.Name);
            }
        }

        var response = await httpClient.PostAsync($"api/knowledge/upload", content);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<DocumentDto>>();
        return result ?? Enumerable.Empty<DocumentDto>();
    }
}

