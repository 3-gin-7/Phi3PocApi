using Phi3PocApi.Enum;

namespace Phi3PocApi.Responses;

public class GenerateResponse
{
    public EResponseStatus ResponseStatusCode { get; set; }
    public string ResponseStatus => ResponseStatusCode.ToString();
    public string? ErrorMessage { get; set; }
    public string Data { get; set; } = null!;
}