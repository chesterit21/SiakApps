using System.ComponentModel.DataAnnotations;

namespace SiakWebApps.Models
{
    public class StudentParent
    {
        public int SiswaId { get; set; }

        public int OrangTuaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Hubungan { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}