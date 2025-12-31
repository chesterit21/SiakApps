using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }

        public int SiswaId { get; set; }

        public int TahunAjaranId { get; set; }

        public int? SemesterId { get; set; }

        [Required]
        [StringLength(50)]
        public string JenisPembayaran { get; set; } = string.Empty;

        public decimal Jumlah { get; set; }

        public DateTime TanggalPembayaran { get; set; } = DateTime.Today;

        [StringLength(255)]
        public string? BuktiPembayaran { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Belum Lunas";

        public int? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}