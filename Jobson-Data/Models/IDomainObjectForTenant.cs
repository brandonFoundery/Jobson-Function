namespace Jobson_Data.Models;

public interface IDomainObjectForTenant
{
    long TenantId { get; set; }
}