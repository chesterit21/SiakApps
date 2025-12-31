using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        public int? UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nis { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Nisn { get; set; }

        [Required]
        [StringLength(100)]
        public string NamaLengkap { get; set; } = string.Empty;

        public int JenisKelaminId { get; set; }

        [StringLength(100)]
        public string? TempatLahir { get; set; }

        public DateTime? TanggalLahir { get; set; }

        [StringLength(50)]
        public string? Agama { get; set; }

        public int? AlamatId { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? FotoProfile { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}