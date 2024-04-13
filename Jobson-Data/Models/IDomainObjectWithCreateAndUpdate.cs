namespace Jobson_Data.Models;

public interface IDomainObjectWithCreateAndUpdate
{
    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public string CreatedById { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public string UpdatedById { get; set; }
}