using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiakWebApps.Models
{
    [Table("menuApp")]
    public class MenuApp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        [Required]
        [StringLength(50)]
        public string UniqueCode { get; set; }

        [StringLength(255)]
        public string? MenuUrl { get; set; }

        public int? ParentMenuId { get; set; }

        public bool IsParent { get; set; }

        public int LevelParent { get; set; }

        // Navigation properties
        [ForeignKey("ParentMenuId")]
        public virtual MenuApp? ParentMenu { get; set; }

        public virtual ICollection<MenuApp> Children { get; set; } = new List<MenuApp>();
    }
}
