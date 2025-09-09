namespace Phi3PocApi.Interfaces;

public interface IPhiService
{
    public (string?,string?) ProcessPrompt(string prompt);
}