using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nip { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NamaLengkap { get; set; } = string.Empty;

        public int JenisKelaminId { get; set; }

        [StringLength(100)]
        public string? TempatLahir { get; set; }

        public DateTime? TanggalLahir { get; set; }

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