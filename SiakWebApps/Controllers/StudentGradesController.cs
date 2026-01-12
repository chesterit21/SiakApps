using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("ACD-GRADE")]
    public class StudentGradesController : BaseController
    {
        private readonly StudentGradeService _studentGradeService;
        private readonly StudentService _studentService;
        private readonly ExamService _examService;

        public StudentGradesController(
            StudentGradeService studentGradeService,
            StudentService studentService,
            ExamService examService)
        {
            _studentGradeService = studentGradeService;
            _studentService = studentService;
            _examService = examService;
        }

        public async Task<IActionResult> Index()
        {
            var grades = await _studentGradeService.GetAllStudentGradesAsync();
            return View(grades);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var grade = await _studentGradeService.GetStudentGradeByIdAsync(id.Value);
            if (grade == null) return NotFound();
            return PartialView("Details", grade);
        }

        private async Task PrepareDropdowns(StudentGrade? grade = null)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var exams = await _examService.GetAllExamsAsync();

            ViewBag.SiswaId = new SelectList(students, "Id", "NamaLengkap", grade?.SiswaId);
            ViewBag.UjianId = new SelectList(exams, "Id", "Id", grade?.UjianId);
        }

        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiswaId,UjianId,Nilai,Catatan")] StudentGrade grade)
        {
            if (ModelState.IsValid)
            {
                await _studentGradeService.CreateStudentGradeAsync(grade);
                return Json(new { success = true });
            }
            await PrepareDropdowns(grade);
            return PartialView("_CreateModal", grade);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var grade = await _studentGradeService.GetStudentGradeByIdAsync(id.Value);
            if (grade == null) return NotFound();
            await PrepareDropdowns(grade);
            return PartialView("_EditModal", grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiswaId,UjianId,Nilai,Catatan,CreatedAt")] StudentGrade grade)
        {
            if (id != grade.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _studentGradeService.UpdateStudentGradeAsync(grade);
                return Json(new { success = true });
            }
            await PrepareDropdowns(grade);
            return PartialView("_EditModal", grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentGradeService.DeleteStudentGradeAsync(id);
            return Json(new { success = true });
        }
    }
}
