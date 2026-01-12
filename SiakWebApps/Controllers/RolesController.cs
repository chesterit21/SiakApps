using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("SYS-ROLE")]

    public class RolesController : BaseController
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return PartialView("Details", role);
        }

        [MenuActionAuthorize("VIEW")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("Name,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateAsync(role);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", role);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return PartialView("_EditModal", role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
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
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
