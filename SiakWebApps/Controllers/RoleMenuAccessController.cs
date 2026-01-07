using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    public class RoleMenuAccessController : BaseController
    {
        private readonly RoleMenuService _roleMenuService;
        private readonly MenuAppService _menuAppService;

        public RoleMenuAccessController(RoleMenuService roleMenuService, MenuAppService menuAppService)
        {
            _roleMenuService = roleMenuService;
            _menuAppService = menuAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleMenuService.GetAllRolesAsync();
            return Json(new { data = roles });
        }

        [HttpGet]
        public async Task<IActionResult> GetMenusByRole(int roleId)
        {
            var menus = await _menuAppService.GetAllAsync();
            var roleMenus = await _roleMenuService.GetRoleMenusByRoleIdAsync(roleId);

            var result = new List<MenuRoleViewModel>();

            foreach (var menu in menus)
            {
                var roleMenu = roleMenus.FirstOrDefault(rm => rm.MenuId == menu.Id);
                result.Add(new MenuRoleViewModel
                {
                    MenuId = menu.Id,
                    MenuName = menu.MenuName,
                    IsView = roleMenu?.IsView ?? false,
                    IsAdd = roleMenu?.IsAdd ?? false,
                    IsEdit = roleMenu?.IsEdit ?? false,
                    IsDelete = roleMenu?.IsDelete ?? false,
                    IsApprove = roleMenu?.IsApprove ?? false,
                    IsDownload = roleMenu?.IsDownload ?? false,
                    IsPrint = roleMenu?.IsPrint ?? false,
                    IsUpload = roleMenu?.IsUpload ?? false
                });
            }

            return Json(new { data = result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoleMenus([FromBody] SaveRoleMenusViewModel model)
        {
            if (model == null || model.Menus == null)
            {
                return Json(new { success = false, message = "Invalid data received." });
            }
            
            // Delete existing records for the role
            await _roleMenuService.DeleteRoleMenusByRoleIdAsync(model.RoleId);

            // Insert new records
            foreach (var menu in model.Menus)
            {
                if (menu.IsView || menu.IsAdd || menu.IsEdit || menu.IsDelete || menu.IsApprove || menu.IsDownload || menu.IsPrint)
                {
                    await _roleMenuService.CreateRoleMenuAsync(new RoleMenu
                    {
                        RoleId = model.RoleId,
                        MenuId = menu.MenuId,
                        IsView = menu.IsView,
                        IsAdd = menu.IsAdd,
                        IsEdit = menu.IsEdit,
                        IsDelete = menu.IsDelete,
                        IsApprove = menu.IsApprove,
                        IsDownload = menu.IsDownload,
                        IsPrint = menu.IsPrint
                    });
                }
            }

            return Json(new { success = true, message = "Role menu access saved successfully" });
        }
    }

    public class SaveRoleMenusViewModel
    {
        public int RoleId { get; set; }
        public List<MenuRoleViewModel> Menus { get; set; }
    }

    public class MenuRoleViewModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApprove { get; set; }
        public bool IsDownload { get; set; }
        public bool IsPrint { get; set; }
        public bool IsUpload { get; set; }
    }
}