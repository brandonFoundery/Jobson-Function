using Jobson.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class UpworkRssFeedUrl: DomainObjectWithCreateAndUpdate
    {
        public string Name { get; set; }
        public string Url { get; set; }
        
        public long ProfileTypeId { get; set; }
        [ForeignKey("ProfileTypeId")]
        public ProfileType ProfileType { get; set; }
    }
}
