namespace Phi3PocApi.Interfaces;

public interface IPhiService
{
    public Task<(string?,string?)> ProcessPrompt(string prompt);
}