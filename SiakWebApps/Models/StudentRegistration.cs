using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class StudentRegistration
    {
        public int Id { get; set; }

        public int? SiswaId { get; set; }

        public int TahunAjaranId { get; set; }

        public int SemesterId { get; set; }

        [Required]
        [StringLength(100)]
        public string NamaLengkap { get; set; } = string.Empty;

        public int JenisKelaminId { get; set; }

        [StringLength(100)]
        public string? TempatLahir { get; set; }

        public DateTime? TanggalLahir { get; set; }

        [Required]
        public string Alamat { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? NamaAyah { get; set; }

        [StringLength(20)]
        public string? PhoneAyah { get; set; }

        [StringLength(100)]
        public string? NamaIbu { get; set; }

        [StringLength(20)]
        public string? PhoneIbu { get; set; }

        [StringLength(100)]
        public string? AsalSekolah { get; set; }

        public decimal? NilaiRaport { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Menunggu";

        public DateTime TanggalDaftar { get; set; } = DateTime.Today;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}