namespace SiakWebApps.Models
{
    public class MenuPermissionDto
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string UniqueCode { get; set; }
        public string MenuUrl { get; set; }
        public int? ParentMenuId { get; set; }
        public bool IsParent { get; set; }
        public int LevelParent { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
}