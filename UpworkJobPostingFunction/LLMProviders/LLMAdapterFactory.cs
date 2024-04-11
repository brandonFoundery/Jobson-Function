using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Enums;

namespace Jobson.LLMProviders
{
    public class LLMAdapterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public LLMAdapterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ILLMAdapter CreateAdapter(LLMProviderEnum providerEnum)
        {
            switch (providerEnum)
            {
                case LLMProviderEnum.OpenAI:
                    return new OpenAIAdapter();
                case LLMProviderEnum.Anthropic:
                    return (ILLMAdapter)_serviceProvider.GetService(typeof(AnthropicAdapter));
                case LLMProviderEnum.Minstril:
                    return new MinstrilAdapter();
                default:
                    throw new ArgumentException("Invalid LLM providerEnum");
            }
        }
    }
}
