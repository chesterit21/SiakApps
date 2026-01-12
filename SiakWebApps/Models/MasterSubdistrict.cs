using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class MasterSubdistrict
    {
        public int Id { get; set; }

        public int KecamatanId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nama { get; set; } = string.Empty;

        public string DistrictName { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public string ProvinceName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}