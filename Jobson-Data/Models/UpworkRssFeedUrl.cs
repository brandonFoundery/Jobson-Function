using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class UpworkRssFeedUrl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Assuming there's an ID column
        public string Name { get; set; }
        public string Url { get; set; }
        public int ProfileTypeId { get; set; }
        [ForeignKey("ProfileTypeId")]
        public ProfileType ProfileType { get; set; }
    }
}
