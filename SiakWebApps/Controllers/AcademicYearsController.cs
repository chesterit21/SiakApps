using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class AcademicYearsController : BaseController
    {
        private readonly AcademicYearService _academicYearService;

        public AcademicYearsController(AcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        // GET: AcademicYears
        public async Task<IActionResult> Index()
        {
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            return View(academicYears);
        }

        // GET: AcademicYears/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var academicYear = await _academicYearService.GetAcademicYearByIdAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        // GET: AcademicYears/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: AcademicYears/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,TahunMulai,TahunSelesai,IsActive,CreatedAt,UpdatedAt")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                await _academicYearService.CreateAcademicYearAsync(academicYear);
                return Json(new { success = true, message = "Academic year created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: AcademicYears/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var academicYear = await _academicYearService.GetAcademicYearByIdAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", academicYear);
        }

        // POST: AcademicYears/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,TahunMulai,TahunSelesai,IsActive,CreatedAt,UpdatedAt")] AcademicYear academicYear)
        {
            if (id != academicYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _academicYearService.UpdateAcademicYearAsync(academicYear);
                return Json(new { success = true, message = "Academic year updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: AcademicYears/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Karena tidak ada metode Delete di service, kita lewati dulu
            return Json(new { success = false, message = "Delete not implemented" });
        }
    }
}