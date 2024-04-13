using Jobson.Models;

namespace Jobson_Data.Models
{
    public abstract class DomainObjectForTenant: DomainObjectWithCreateAndUpdate, IDomainObjectForTenant
    {
        public long TenantId { get; set; }
    }
}
