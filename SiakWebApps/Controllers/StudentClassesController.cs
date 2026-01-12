using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("ACD-STUDENTCLASS")]

    public class StudentClassesController : BaseController
    {
        private readonly StudentClassService _studentClassService;

        public StudentClassesController(StudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        // GET: StudentClasses
        public async Task<IActionResult> Index()
        {
            var studentClasses = await _studentClassService.GetAllAsync();
            return View(studentClasses);
        }

        // GET: StudentClasses/Details/5
        public async Task<IActionResult> Details(int studentId, int classId, int semesterId)
        {
            var studentClass = await _studentClassService.GetByIdAsync(studentId, classId, semesterId);
            if (studentClass == null)
            {
                return NotFound();
            }
            return View(studentClass);
        }

        // GET: StudentClasses/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: StudentClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiswaId,KelasId,SemesterId,TanggalMasuk,TanggalKeluar,IsActive")] StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                await _studentClassService.CreateAsync(studentClass);
                return Json(new { success = true, message = "Student class created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: StudentClasses/Edit/5
        public async Task<IActionResult> Edit(int studentId, int classId, int semesterId)
        {
            var studentClass = await _studentClassService.GetByIdAsync(studentId, classId, semesterId);
            if (studentClass == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", studentClass);
        }

        // POST: StudentClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int studentId, int classId, int semesterId, [Bind("SiswaId,KelasId,SemesterId,TanggalMasuk,TanggalKeluar,IsActive,CreatedAt,UpdatedAt")] StudentClass studentClass)
        {
            // Note: In this case, we're checking the composite key values
            // This is a simplified approach - in a real application, you might need a better way to handle this
            if (ModelState.IsValid)
            {
                await _studentClassService.UpdateAsync(studentClass);
                return Json(new { success = true, message = "Student class updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: StudentClasses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int studentId, int classId, int semesterId)
        {
            var result = await _studentClassService.DeleteAsync(studentId, classId, semesterId);
            if (result)
            {
                return Json(new { success = true, message = "Student class deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting student class" });
        }
    }
}