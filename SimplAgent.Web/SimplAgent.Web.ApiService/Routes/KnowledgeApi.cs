using Microsoft.AspNetCore.Mvc;
using NTG.Agent.Orchestrator.Services.Knowledge;

namespace SimplAgent.Web.ApiService.Routes;

public static class KnowledgeApi
{
    public static void MapKnowledgeApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/knowledge")
            .WithOpenApi()
            .WithTags("Knowledge");

        // POST /api/knowledge
        group.MapPost("/upload", AddKnowledge)
            .DisableAntiforgery()
            .WithName("AddKnowledge")
            .WithDescription("Add knowledge to knowledge memory.")
            .Produces(200)
            .Produces(400);
    }

    private static async Task<IResult> AddKnowledge(
        [FromForm] IFormFileCollection files,
        [FromServices] IKnowledgeService knowledgeService)
    {
        if (files == null || files.Count == 0)
        {
            return Results.BadRequest(new { message = "No files uploaded." });
        }

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                await knowledgeService.ImportDocumentAsync(file.OpenReadStream(), file.FileName);
            }
        }

        return Results.Ok(new { message = "Files uploaded successfully." });
    }
}
