using System.ComponentModel.DataAnnotations;

namespace Phi3PocApi.Requests;

public class GenerateRequest
{
    public string Prompt { get; set; } = null!;
}