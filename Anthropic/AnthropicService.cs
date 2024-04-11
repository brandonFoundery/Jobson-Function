using System.Text.Json;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using Anthropic.SDK;

namespace Anthropic
{
    public class AnthropicService
    {
        public async Task<string> SendMessage(string message)
        {
            var client = new AnthropicClient();
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
                MaxTokens = 512,
                Model = AnthropicModels.Claude3Sonnet,
                Stream = false,
                Temperature = 1.0m,
            };
            var res = await client.Messages.GetClaudeMessageAsync(parameters).ConfigureAwait(false);

            if (res == null)
            {
                throw new Exception("Failed to get response from Anthropic API");
            }

            //Return res as JSON because main function cannot reference the Anthropic SDK
            return JsonSerializer.Serialize(res);
        }
    }
}
