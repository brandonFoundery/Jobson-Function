using System.ComponentModel.DataAnnotations.Schema;
using Jobson_Data.Enums;

namespace Jobson_Data.Models
{
    public class LLMModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //Name
        public string Name { get; set; }
        public LLMProviderEnum LlmProviderType { get; set; }
        //key
        public string ApiKey { get; set; }
    }
}
