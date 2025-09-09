using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntimeGenAI;
using Phi3PocApi.Interfaces;
using Phi3PocApi.Requests;
using Phi3PocApi.Responses;

namespace Phi3PocApi.Extensions;

public static class ApiExtensions
{
    public static void RegisterApiEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api");

        group.MapPost("/generate", GenerateText)
        .WithDisplayName("Generate");
    }

    private static async Task<IResult> GenerateText(
        [FromServices] Model model,
        [FromServices] Tokenizer tokenizer,
        [FromServices] IPhiService phiService,
        GenerateRequest request
        )
    {
        var prompt = $"<|user|>{request.Prompt}<|end|><|assistant|>";

        var (data, err) = await phiService.ProcessPrompt(prompt);

        if (!string.IsNullOrEmpty(err))
        {
            return Results.Ok(new GenerateResponse()
            {
                ResponseStatusCode = Enum.EResponseStatus.Failure,
                ErrorMessage = err
            });
        }

        return Results.Ok(new GenerateResponse()
        {
            ResponseStatusCode = Enum.EResponseStatus.Success,
            Data = data ?? ""
        });
    }
}