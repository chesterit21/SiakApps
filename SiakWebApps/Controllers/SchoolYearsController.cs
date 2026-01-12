using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-SCHOOLYEAR")]

    public class SchoolYearsController : BaseController
    {
        private readonly SchoolYearService _schoolYearService;

        public SchoolYearsController(SchoolYearService schoolYearService)
        {
            _schoolYearService = schoolYearService;
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var schoolYears = await _schoolYearService.GetAllAsync();
            return View(schoolYears);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int id)
        {
            var schoolYear = await _schoolYearService.GetByIdAsync(id);
            if (schoolYear == null) return NotFound();
            return PartialView("Details", schoolYear);
        }

        [MenuActionAuthorize("VIEW")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("Nama,TahunMulai,TahunSelesai,IsActive")] SchoolYear schoolYear)
        {
            if (ModelState.IsValid)
            {
                await _schoolYearService.CreateAsync(schoolYear);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", schoolYear);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int id)
        {
            var schoolYear = await _schoolYearService.GetByIdAsync(id);
            if (schoolYear == null) return NotFound();
            return PartialView("_EditModal", schoolYear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,TahunMulai,TahunSelesai,IsActive,CreatedAt")] SchoolYear schoolYear)
        {
            if (id != schoolYear.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _schoolYearService.UpdateAsync(schoolYear);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", schoolYear);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _schoolYearService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
