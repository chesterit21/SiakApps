using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class TeachersController : Controller
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return View(teachers);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Nip,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,AlamatId,Phone,Email,FotoProfile")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await _teacherService.CreateTeacherAsync(teacher);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Nip,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,AlamatId,Phone,Email,FotoProfile,CreatedAt")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _teacherService.UpdateTeacherAsync(teacher);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return Json(new { success = true });
        }
    }
}
