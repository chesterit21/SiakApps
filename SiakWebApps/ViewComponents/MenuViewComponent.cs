using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using System.Text.Json;

namespace SiakWebApps.ViewComponents
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public string? MenuUrl { get; set; }
        public List<MenuItemViewModel> Children { get; set; } = new List<MenuItemViewModel>();
    }

    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuJson = HttpContext.Session.GetString("UserMenu");

            List<MenuItemViewModel> menu = new List<MenuItemViewModel>();

            if (!string.IsNullOrEmpty(menuJson))
            {
                var flatMenu = JsonSerializer.Deserialize<IEnumerable<MenuApp>>(menuJson) ?? new List<MenuApp>();
                menu = BuildMenuHierarchy(flatMenu.ToList());
            }
            
            return View(menu);
        }

        private List<MenuItemViewModel> BuildMenuHierarchy(List<MenuApp> flatMenu, int? parentId = null)
        {
            var topLevelItems = new List<MenuItemViewModel>();
            var children = flatMenu.Where(m => m.ParentMenuId == parentId).ToList();

            foreach (var item in children)
            {
                var menuItem = new MenuItemViewModel
                {
                    Id = item.Id,
                    MenuName = item.MenuName,
                    MenuUrl = item.MenuUrl
                };

                if (item.IsParent)
                {
                    menuItem.Children = BuildMenuHierarchy(flatMenu, item.Id);
                }
                topLevelItems.Add(menuItem);
            }
            return topLevelItems;
        }
    }
}
