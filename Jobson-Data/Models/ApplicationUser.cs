using System.ComponentModel.DataAnnotations.Schema;
using Jobson.Models;
using Microsoft.AspNetCore.Identity;

namespace Jobson_Data.Models
{
    public class ApplicationUser : IdentityUser, IDomainObjectForTenant
    {

        public long TenantId { get; set; } = 0;
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
    }
}