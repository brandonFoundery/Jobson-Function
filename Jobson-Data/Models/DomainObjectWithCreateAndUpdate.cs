using System.ComponentModel.DataAnnotations;

namespace Jobson_Data.Models
{
    public abstract class DomainObjectWithCreateAndUpdate : DomainObjectBase, IDomainObjectWithCreateAndUpdate
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string CreatedById { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public string UpdatedById { get; set; }

        public DateTime? ValidUntilDate { get; set; } 
    }
}
