using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }

        public int SiswaId { get; set; }

        public int UjianId { get; set; }

        public decimal Nilai { get; set; }

        public string? Catatan { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}