using System.ComponentModel.DataAnnotations.Schema;
using Jobson.Models;

namespace Jobson_Data.Models
{
    public class UpworkProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProfileTypeId { get; set; }
        [ForeignKey("ProfileTypeId")]
        public ProfileType ProfileType { get; set; }

        public string Url { get; set; }

        //Stores the content of the profile page
        public string ProfileContent { get; set; }
        public string UpworkProfileKey { get; set; }
        //Reference TrelloBoard
        public string TrelloBoardId { get; set; }
        [ForeignKey("TrelloBoardId")]
        public TrelloBoard TrelloBoard { get; set; }

        //Map to Job
        public List<Job> Jobs { get; set; }

    }
}
