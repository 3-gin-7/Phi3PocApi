using Microsoft.ML.OnnxRuntimeGenAI;
using Phi3PocApi.BackgroundService;
using Phi3PocApi.Interfaces;
using Phi3PocApi.Services;

namespace Phi3PocApi.Extensions;

public static class PhiExtensions
{
    public static IServiceCollection AddPhiModel(this IServiceCollection svc, string modelPath)
    {
        Console.WriteLine($"Loading model from path: {modelPath}");

        svc.AddSingleton(_ => new Model(modelPath));
        svc.AddSingleton(_ => new Tokenizer(_.GetRequiredService<Model>()));

        svc.AddHostedService<ModelLoader>();
        svc.AddTransient<IPhiService, PhiService>();

        return svc;
    }
}