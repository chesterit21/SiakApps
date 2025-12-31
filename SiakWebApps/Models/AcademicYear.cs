using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nama { get; set; } = string.Empty;

        public int TahunMulai { get; set; }

        public int TahunSelesai { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}