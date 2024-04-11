using System.Threading.Tasks;

namespace Jobson.LLMProviders;

public class MinstrilAdapter : ILLMAdapter
{
    public async Task<string> SendMessage(string message)
    {
        // Minstril-specific implementation to send a message
        // Return the response from Minstril
        return $"Minstril Response: {message}";
    }
}