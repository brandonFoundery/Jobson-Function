using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobson.LLMProviders
{
    public interface ILLMAdapter
    {
        Task<string> SendMessage(string message);
    }
}
