using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Parent
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nik { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NamaLengkap { get; set; } = string.Empty;

        public int JenisKelaminId { get; set; }

        [StringLength(100)]
        public string? TempatLahir { get; set; }

        public DateTime? TanggalLahir { get; set; }

        [StringLength(50)]
        public string? Agama { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Pekerjaan { get; set; }

        public decimal? PenghasilanPerBulan { get; set; }

        public int? AlamatId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}