using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class LLMPrompt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //description
        public string LLMPromptType { get; set; }
        public int LLMProviderId { get; set; }
        [ForeignKey("LLMProviderId")]
        public LLMModel LLMProvider { get; set; }
        public string PromptText { get; set; }
    }
}
