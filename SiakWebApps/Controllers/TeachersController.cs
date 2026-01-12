using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-GURU")]

    public class TeachersController : BaseController
    {
        private readonly TeacherService _teacherService;
        private readonly UserService _userService;

        public TeachersController(TeacherService teacherService, UserService userService)
        {
            _teacherService = teacherService;
            _userService = userService;
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
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync();
            ViewBag.UserId = new SelectList(users, "Id", "Username");
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
            var users = await _userService.GetAllUsersAsync();
            ViewBag.UserId = new SelectList(users, "Id", "Username", teacher.UserId);
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
            var users = await _userService.GetAllUsersAsync();
            ViewBag.UserId = new SelectList(users, "Id", "Username", teacher.UserId);
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
            var users = await _userService.GetAllUsersAsync();
            ViewBag.UserId = new SelectList(users, "Id", "Username", teacher.UserId);
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
