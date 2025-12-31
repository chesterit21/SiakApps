using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class SemestersController : Controller
    {
        private readonly SemesterService _semesterService;
        private readonly AcademicYearService _academicYearService;

        public SemestersController(SemesterService semesterService, AcademicYearService academicYearService)
        {
            _semesterService = semesterService;
            _academicYearService = academicYearService;
        }

        public async Task<IActionResult> Index()
        {
            var semesters = await _semesterService.GetAllSemestersAsync();
            return View(semesters);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var semester = await _semesterService.GetSemesterByIdAsync(id.Value);
            if (semester == null) return NotFound();
            return PartialView("Details", semester);
        }

        public async Task<IActionResult> Create()
        {
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama");
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TahunAjaranId,Nama,SemesterKe,TanggalMulai,TanggalSelesai,IsActive")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                await _semesterService.CreateSemesterAsync(semester);
                return Json(new { success = true });
            }
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", semester.TahunAjaranId);
            return PartialView("_CreateModal", semester);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var semester = await _semesterService.GetSemesterByIdAsync(id.Value);
            if (semester == null) return NotFound();

            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", semester.TahunAjaranId);
            return PartialView("_EditModal", semester);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TahunAjaranId,Nama,SemesterKe,TanggalMulai,TanggalSelesai,IsActive,CreatedAt")] Semester semester)
        {
            if (id != semester.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _semesterService.UpdateSemesterAsync(semester);
                return Json(new { success = true });
            }
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", semester.TahunAjaranId);
            return PartialView("_EditModal", semester);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _semesterService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
