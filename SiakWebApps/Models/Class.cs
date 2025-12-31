using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nama { get; set; } = string.Empty;

        public int Tingkat { get; set; }

        public int TahunAjaranId { get; set; }

        public int? WaliKelasId { get; set; }

        public int Kapasitas { get; set; } = 30;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}