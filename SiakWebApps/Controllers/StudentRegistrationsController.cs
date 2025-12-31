using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class StudentRegistrationsController : Controller
    {
        private readonly StudentRegistrationService _registrationService;
        private readonly AcademicYearService _academicYearService;
        private readonly SemesterService _semesterService;
        private readonly MasterGenderService _genderService;

        public StudentRegistrationsController(
            StudentRegistrationService registrationService,
            AcademicYearService academicYearService,
            SemesterService semesterService,
            MasterGenderService genderService)
        {
            _registrationService = registrationService;
            _academicYearService = academicYearService;
            _semesterService = semesterService;
            _genderService = genderService;
        }

        public async Task<IActionResult> Index()
        {
            var registrations = await _registrationService.GetAllAsync();
            return View(registrations);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var registration = await _registrationService.GetByIdAsync(id.Value);
            if (registration == null) return NotFound();
            return PartialView("Details", registration);
        }

        private async Task PrepareDropdowns(StudentRegistration? registration = null)
        {
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            var semesters = await _semesterService.GetAllSemestersAsync();
            var genders = await _genderService.GetAllAsync();

            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", registration?.TahunAjaranId);
            ViewBag.SemesterId = new SelectList(semesters, "Id", "Nama", registration?.SemesterId);
            ViewBag.JenisKelaminId = new SelectList(genders, "Id", "Nama", registration?.JenisKelaminId);
        }

        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TahunAjaranId,SemesterId,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,Alamat,Phone,Email,NamaAyah,PhoneAyah,NamaIbu,PhoneIbu,AsalSekolah,NilaiRaport,Status,TanggalDaftar")] StudentRegistration registration)
        {
            if (ModelState.IsValid)
            {
                await _registrationService.CreateAsync(registration);
                return Json(new { success = true });
            }
            await PrepareDropdowns(registration);
            return PartialView("_CreateModal", registration);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var registration = await _registrationService.GetByIdAsync(id.Value);
            if (registration == null) return NotFound();
            await PrepareDropdowns(registration);
            return PartialView("_EditModal", registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiswaId,TahunAjaranId,SemesterId,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,Alamat,Phone,Email,NamaAyah,PhoneAyah,NamaIbu,PhoneIbu,AsalSekolah,NilaiRaport,Status,TanggalDaftar,CreatedAt")] StudentRegistration registration)
        {
            if (id != registration.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _registrationService.UpdateAsync(registration);
                return Json(new { success = true });
            }
            await PrepareDropdowns(registration);
            return PartialView("_EditModal", registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _registrationService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
