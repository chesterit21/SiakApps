using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MenuAppsController : BaseController
    {
        private readonly MenuAppService _menuAppService;

        public MenuAppsController(MenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        public async Task<IActionResult> Index()
        {
            var menuApps = await _menuAppService.GetAllAsync();
            return View(menuApps);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuApps()
        {
            var menuApps = await _menuAppService.GetAllAsync();
            return Json(new { data = menuApps });
        }

        [HttpGet]
        public async Task<IActionResult> GetParentMenus()
        {
            var parentMenus = await _menuAppService.GetParentMenusAsync();
            return Json(parentMenus);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var menuApp = await _menuAppService.GetByIdAsync(id);
            if (menuApp == null)
            {
                return NotFound();
            }
            return Json(menuApp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuApp menuApp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _menuAppService.CreateAsync(menuApp);
                    return Json(new { success = true, message = "Menu created successfully" });
                }
                return Json(new { success = false, message = "Invalid data", errors = ModelState });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuApp menuApp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updated = await _menuAppService.UpdateAsync(menuApp);
                    if (updated)
                    {
                        return Json(new { success = true, message = "Menu updated successfully" });
                    }
                }
                return Json(new { success = false, message = "Invalid data", errors = ModelState });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _menuAppService.DeleteAsync(id);
            if (deleted)
            {
                return Json(new { success = true, message = "Menu deleted successfully" });
            }
            return Json(new { success = false, message = "Menu not found" });
        }
    }
}