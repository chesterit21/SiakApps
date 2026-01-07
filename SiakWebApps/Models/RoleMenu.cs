using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiakWebApps.Models
{
    [Table("role_menu")]
    public class RoleMenu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int MenuId { get; set; }

        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpload { get; set; }
        public bool IsApprove { get; set; }
        public bool IsDownload { get; set; }
        public bool IsPrint { get; set; }

        // Navigation properties
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        [ForeignKey("MenuId")]
        public virtual MenuApp? MenuApp { get; set; }
    }
}
