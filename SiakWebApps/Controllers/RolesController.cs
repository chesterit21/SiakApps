using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return PartialView("Details", role);
        }

        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateAsync(role);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", role);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return PartialView("_EditModal", role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedAt")] Role role)
        {
            if (id != role.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _roleService.UpdateAsync(role);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
