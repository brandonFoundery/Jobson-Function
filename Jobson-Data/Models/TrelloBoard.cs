using System.ComponentModel.DataAnnotations;

namespace Jobson_Data.Models
{
    public class TrelloBoard
    {
        [Key]
        public string BoardId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
