
using System.Diagnostics;
using Microsoft.ML.OnnxRuntimeGenAI;

namespace Phi3PocApi.BackgroundService;

public class ModelLoader : IHostedService
{
    private readonly IServiceProvider _sp;

    public ModelLoader(IServiceProvider sp)
    {
        _sp = sp;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        Console.WriteLine("Loading model");
        sw.Start();
        using var scope = _sp.CreateScope();
        var model = scope.ServiceProvider.GetRequiredService<Model>();
        var tokenizer = scope.ServiceProvider.GetRequiredService<Tokenizer>();

        sw.Stop();
        Console.WriteLine($"Done loading model in: {sw.Elapsed}");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}