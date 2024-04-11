using System.Collections.Generic;
using System.Threading.Tasks;
using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Jobson.LLMProviders;

public class AnthropicAdapter : ILLMAdapter
{
    private readonly IOptions<AppSettings> _appSettings;

    public AnthropicAdapter(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings;
    }
    
    public async Task<string> SendMessage(string message)
    {
        string anthropic_key = _appSettings.Value.Anthropic_Key;
        var client = new AnthropicClient(anthropic_key);
        var messages = new List<Message>
        {
            new Message()
            {
                Role = RoleType.User,
                Content = message
            }
        };
        var parameters = new MessageParameters()
        {
            Messages = messages,
            MaxTokens = 2000,
            Model = AnthropicModels.Claude3Sonnet,
            Stream = false,
            Temperature = 1.0m,
        };
        var res = await client.Messages.GetClaudeMessageAsync(parameters);
        return res.Content.ToString();
    }
}