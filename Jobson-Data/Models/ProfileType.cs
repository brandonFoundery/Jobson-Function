using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class ProfileType: DomainObjectWithCreateAndUpdate
    {
        //Name of the profile type
        public string Name { get; set; }
    }
}
