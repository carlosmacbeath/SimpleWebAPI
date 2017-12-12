using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AmenityGroup
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string  GroupName { get; set; }
        public bool SortOrder { get; set; }
        public bool Deleted { get; set; }

        public ICollection<Amenity> Student { get; set; }
    }
}
