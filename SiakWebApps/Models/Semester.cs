using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Semester
    {
        public int Id { get; set; }

        public int TahunAjaranId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nama { get; set; } = string.Empty;

        public int SemesterKe { get; set; }

        public DateTime TanggalMulai { get; set; }

        public DateTime TanggalSelesai { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}