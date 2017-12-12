using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string AmenityName { get; set; }
        public bool SortOrder { get; set; }
        public bool Deleted { get; set; }
        public bool ShowOnSearch { get; set; }
        public int SearchSortOrder { get; set; }

        public int? AmenityGroupId { get; set; }
        public AmenityGroup AmenityGroup { get; set; }

    }
}