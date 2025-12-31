using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class StudentScholarship
    {
        public int SiswaId { get; set; }

        public int BeasiswaId { get; set; }

        public int TahunAjaranId { get; set; }

        public int SemesterId { get; set; }

        public DateTime TanggalMulai { get; set; }

        public DateTime? TanggalSelesai { get; set; }

        public decimal? JumlahBantuan { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Aktif";

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}