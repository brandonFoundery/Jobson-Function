using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class Job: DomainObjectWithCreateAndUpdate
    {
        public long ProfileTypeId { get; set; }
        [ForeignKey("ProfileTypeId")]
        public ProfileType ProfileType { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public DateTime PubDate { get; set; }
        // Trello Properties
        public DateTime BatchRunTime { get; set; }
        public string CoverLetter { get; set; }
        public string TrelloCardId { get; set; }
        public string TrelloCardUrl { get; set; }
        public string TrelloListId { get; set; }
        public string TrelloListName { get; set; }
        public string TrelloBoardId { get; set; }
        public string TrelloBoardName { get; set; }
        public string TrelloCardShortUrl { get; set; }

        // Upwork Properties
        public string UpworkJobId { get; set; }
        public string UpworkJobUrl { get; set; }
        public string UpworkJobTitle { get; set; }
        public string UpworkJobDescription { get; set; }
        public string UpworkJobCategory { get; set; }


        public string Content { get; set; }

        //Map to UpworkProfile
        public long? UpworkProfileId { get; set; }
        [ForeignKey("UpworkProfileId")]
        public UpworkProfile? UpworkProfile { get; set; }
    }
}