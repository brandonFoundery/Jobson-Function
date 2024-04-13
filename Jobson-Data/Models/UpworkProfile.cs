using System.ComponentModel.DataAnnotations.Schema;
using Jobson.Models;

namespace Jobson_Data.Models
{
    public class UpworkProfile: DomainObjectWithCreateAndUpdate
    {
        public string Name { get; set; }

        public long ProfileTypeId { get; set; }
        [ForeignKey("ProfileTypeId")]
        public ProfileType ProfileType { get; set; }

        public string Url { get; set; }

        //Stores the content of the profile page
        public string ProfileContent { get; set; }
        public string UpworkProfileKey { get; set; }

        // Explicitly defining the navigation property back to Job
        public List<Job> Jobs { get; set; } = new List<Job>();  // Initialize to prevent null issues


    }
}
