using Microsoft.AspNetCore.Mvc;
using SimplAgent.Shared.Dtos.Contracts;

namespace SimplAgent.Web.ApiService.Routes;

public static class KnowledgeApi
{
    public static void MapKnowledgeApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/knowledge")
            .WithOpenApi()
            .WithTags("Knowledge");

        // POST /api/knowledge
        group.MapPost("/upload", UploadKnowledge)
            .DisableAntiforgery()
            .WithName("UploadKnowledge")
            .WithDescription("Add knowledge to knowledge memory by uploading a file.")
            .Produces(200)
            .Produces(400);

        group.MapGet("/documents", GetAllDocuments)
            .WithName("GetAllKnowledgeDocuments")
            .WithDescription("Retrieve all knowledge documents.")
            .Produces<IEnumerable<DocumentDto>>(200);

        group.MapDelete("/documents/{documentId}", DeleteDocument)
            .WithName("DeleteKnowledgeDocument")
            .WithDescription("Delete a knowledge document by its ID.")
            .Produces(200)
            .Produces(404);
    }

    private static async Task<IResult> DeleteDocument(
        [FromRoute] Guid documentId,
        [FromServices] IKnowledgeService knowledgeService)
    {
        try
        {
            await knowledgeService.RemoveDocumentAsync(documentId);
            return Results.Ok(new { message = "Document deleted successfully." });
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound(new { message = "Document not found." });
        }
    }

    private static async Task<IResult> GetAllDocuments(
        [FromServices] IKnowledgeService knowledgeService)
    {
        var documents = await knowledgeService.GetAllDocumentsAsync();
        return Results.Ok(documents);
    }

    private static async Task<IResult> UploadKnowledge(
        [FromForm] IFormFileCollection files,
        [FromServices] IKnowledgeService knowledgeService)
    {
        if (files == null || files.Count == 0)
        {
            return Results.BadRequest(new { message = "No files uploaded." });
        }

        var documents = new List<DocumentDto>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var document= await knowledgeService.ImportDocumentAsync(file.OpenReadStream(), file.FileName);
                documents.Add(document);
            }
        }

        return Results.Ok(documents);
    }
}
