using System.Threading.Tasks;

namespace Jobson.LLMProviders;

public class OpenAIAdapter : ILLMAdapter
{
    public async Task<string> SendMessage(string message)
    {
        // OpenAI-specific implementation to send a message
        // Return the response from OpenAI
        return $"OpenAI Response: {message}";
    }
}