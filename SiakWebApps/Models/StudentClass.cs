using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class StudentClass
    {
        public int SiswaId { get; set; }

        public int KelasId { get; set; }

        public int SemesterId { get; set; }

        public DateTime TanggalMasuk { get; set; } = DateTime.Today;

        public DateTime? TanggalKeluar { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}