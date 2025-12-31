using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class StudentScholarshipsController : Controller
    {
        private readonly StudentScholarshipService _studentScholarshipService;
        private readonly StudentService _studentService;
        private readonly AcademicYearService _academicYearService;
        private readonly SemesterService _semesterService;

        public StudentScholarshipsController(
            StudentScholarshipService studentScholarshipService,
            StudentService studentService,
            AcademicYearService academicYearService,
            SemesterService semesterService)
        {
            _studentScholarshipService = studentScholarshipService;
            _studentService = studentService;
            _academicYearService = academicYearService;
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index()
        {
            var scholarships = await _studentScholarshipService.GetAllAsync();
            return View(scholarships);
        }

        public async Task<IActionResult> Details(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            var scholarship = await _studentScholarshipService.GetByIdAsync(studentId, scholarshipId, academicYearId, semesterId);
            if (scholarship == null) return NotFound();
            return PartialView("Details", scholarship);
        }

        private async Task PrepareDropdowns(StudentScholarship? scholarship = null)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            var semesters = await _semesterService.GetAllSemestersAsync();

            ViewBag.SiswaId = new SelectList(students, "Id", "NamaLengkap", scholarship?.SiswaId);
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", scholarship?.TahunAjaranId);
            ViewBag.SemesterId = new SelectList(semesters, "Id", "Nama", scholarship?.SemesterId);

            // Mocking BeasiswaId for now
            ViewBag.BeasiswaId = new SelectList(new[]
            {
                new { Id = 1, Name = "Prestasi" },
                new { Id = 2, Name = "Ekonomi (KIP)" },
                new { Id = 3, Name = "Yayasan" }
            }, "Id", "Name", scholarship?.BeasiswaId);
        }

        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiswaId,BeasiswaId,TahunAjaranId,SemesterId,TanggalMulai,TanggalSelesai,JumlahBantuan,Status")] StudentScholarship scholarship)
        {
            if (ModelState.IsValid)
            {
                await _studentScholarshipService.CreateAsync(scholarship);
                return Json(new { success = true });
            }
            await PrepareDropdowns(scholarship);
            return PartialView("_CreateModal", scholarship);
        }

        public async Task<IActionResult> Edit(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            var scholarship = await _studentScholarshipService.GetByIdAsync(studentId, scholarshipId, academicYearId, semesterId);
            if (scholarship == null) return NotFound();
            await PrepareDropdowns(scholarship);
            return PartialView("_EditModal", scholarship);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SiswaId,BeasiswaId,TahunAjaranId,SemesterId,TanggalMulai,TanggalSelesai,JumlahBantuan,Status,CreatedAt")] StudentScholarship scholarship)
        {
            if (ModelState.IsValid)
            {
                await _studentScholarshipService.UpdateAsync(scholarship);
                return Json(new { success = true });
            }
            await PrepareDropdowns(scholarship);
            return PartialView("_EditModal", scholarship);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            await _studentScholarshipService.DeleteAsync(studentId, scholarshipId, academicYearId, semesterId);
            return Json(new { success = true });
        }
    }
}
