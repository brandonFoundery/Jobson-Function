using System.ComponentModel.DataAnnotations.Schema;
using Jobson_Data.Models;

namespace Jobson.Models
{
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        List<ApplicationUser> Users { get; set; }

    }
}
