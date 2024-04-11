using System.ComponentModel.DataAnnotations.Schema;

namespace Jobson_Data.Models
{
    public class ProfileType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Id
        public int Id { get; set; }
        //Name of the profile type
        public string Name { get; set; }
    }
}
