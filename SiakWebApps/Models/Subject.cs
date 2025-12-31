using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nama { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Kode { get; set; } = string.Empty;

        public int? TingkatMin { get; set; }

        public int? TingkatMax { get; set; }

        public string? Deskripsi { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}