using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public int? AnggaranId { get; set; }

        public int TahunAjaranId { get; set; }

        public int? SemesterId { get; set; }

        [Required]
        [StringLength(100)]
        public string NamaPengeluaran { get; set; } = string.Empty;

        public decimal Jumlah { get; set; }

        public string? Deskripsi { get; set; }

        public DateTime TanggalPengeluaran { get; set; } = DateTime.Today;

        [StringLength(255)]
        public string? BuktiPengeluaran { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}