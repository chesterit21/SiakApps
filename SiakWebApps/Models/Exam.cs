using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Exam
    {
        public int Id { get; set; }

        public int JenisUjianId { get; set; }

        public int MataPelajaranId { get; set; }

        public int KelasId { get; set; }

        public int SemesterId { get; set; }

        public DateTime TanggalUjian { get; set; }

        public TimeSpan JamMulai { get; set; }

        public TimeSpan JamSelesai { get; set; }

        public int? RuanganId { get; set; }

        public int GuruId { get; set; }

        public string? Deskripsi { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}