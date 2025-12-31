using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nama { get; set; } = string.Empty;

        public int Kapasitas { get; set; } = 30;

        [StringLength(100)]
        public string? Lokasi { get; set; }

        [StringLength(50)]
        public string TipeRuangan { get; set; } = "Kelas";

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}