using System.Diagnostics;
using System.Text;
using Microsoft.ML.OnnxRuntimeGenAI;
using Phi3PocApi.Interfaces;

namespace Phi3PocApi.Services;

public class PhiService : IPhiService
{
    private readonly Model _model;
    private readonly Tokenizer _tokenizer;

    public PhiService(Model model, Tokenizer tokenizer)
    {
        _model = model;
        _tokenizer = tokenizer;
    }

    public async Task<(string?,string?)> ProcessPrompt(string prompt)
    {
        var sw = new Stopwatch();

        try
        {
            sw.Start();
            Console.WriteLine($"prompt is: {prompt}");

            var tokens = _tokenizer.Encode(prompt);

            var generatorParams = new GeneratorParams(_model);
            generatorParams.SetSearchOption("max_length", 2048);
            generatorParams.SetInputSequences(tokens);
            generatorParams.TryGraphCaptureWithMaxBatchSize(1);

            using var tokenizerStream = _tokenizer.CreateStream();
            using Generator generator = new(_model, generatorParams);

            StringBuilder stringBuilder = new StringBuilder();
            while (!generator.IsDone())
            {
                string part;
                generator.ComputeLogits();
                generator.GenerateNextToken();

                part = tokenizerStream.Decode(generator.GetSequence(0)[^1]);
                stringBuilder.Append(part);
            }

            sw.Stop();
            Console.WriteLine($"Done in {sw.Elapsed}");

            return (stringBuilder.ToString().TrimStart(), null);
        }
        catch (Exception ex)
        {
            return (null, ex.InnerException?.Message ?? ex.Message);
        }
        finally
        {
            sw.Stop();
        }
    }
}