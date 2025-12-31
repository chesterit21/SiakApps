using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class MasterCity
    {
        public int Id { get; set; }

        public int ProvinsiId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nama { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}