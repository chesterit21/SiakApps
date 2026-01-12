using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("ACD-EXAM")]
    public class ExamsController : BaseController
    {
        private readonly ExamService _examService;
        private readonly SubjectService _subjectService;
        private readonly ClassService _classService;
        private readonly SemesterService _semesterService;
        private readonly RoomService _roomService;
        private readonly TeacherService _teacherService;

        public ExamsController(
            ExamService examService,
            SubjectService subjectService,
            ClassService classService,
            SemesterService semesterService,
            RoomService roomService,
            TeacherService teacherService)
        {
            _examService = examService;
            _subjectService = subjectService;
            _classService = classService;
            _semesterService = semesterService;
            _roomService = roomService;
            _teacherService = teacherService;
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var exams = await _examService.GetAllExamsAsync();
            return View(exams);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var exam = await _examService.GetExamByIdAsync(id.Value);
            if (exam == null) return NotFound();
            return PartialView("Details", exam);
        }

        private async Task PrepareDropdowns(Exam? exam = null)
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            var classes = await _classService.GetAllClassesAsync();
            var semesters = await _semesterService.GetAllSemestersAsync();
            var rooms = await _roomService.GetAllAsync();
            var teachers = await _teacherService.GetAllTeachersAsync();

            ViewBag.MataPelajaranId = new SelectList(subjects, "Id", "Nama", exam?.MataPelajaranId);
            ViewBag.KelasId = new SelectList(classes, "Id", "Nama", exam?.KelasId);
            ViewBag.SemesterId = new SelectList(semesters, "Id", "Nama", exam?.SemesterId);
            ViewBag.RuanganId = new SelectList(rooms, "Id", "Nama", exam?.RuanganId);
            ViewBag.GuruId = new SelectList(teachers, "Id", "NamaLengkap", exam?.GuruId);

            // Mocking JenisUjianId for now (could be an enum or table)
            ViewBag.JenisUjianId = new SelectList(new[]
            {
                new { Id = 1, Name = "Quiz" },
                new { Id = 2, Name = "Midterm (UTS)" },
                new { Id = 3, Name = "Final (UAS)" }
            }, "Id", "Name", exam?.JenisUjianId);
        }

        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("JenisUjianId,MataPelajaranId,KelasId,SemesterId,TanggalUjian,JamMulai,JamSelesai,RuanganId,GuruId,Deskripsi")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                await _examService.CreateExamAsync(exam);
                return Json(new { success = true });
            }
            await PrepareDropdowns(exam);
            return PartialView("_CreateModal", exam);
        }

        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var exam = await _examService.GetExamByIdAsync(id.Value);
            if (exam == null) return NotFound();
            await PrepareDropdowns(exam);
            return PartialView("_EditModal", exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JenisUjianId,MataPelajaranId,KelasId,SemesterId,TanggalUjian,JamMulai,JamSelesai,RuanganId,GuruId,Deskripsi,CreatedAt")] Exam exam)
        {
            if (id != exam.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _examService.UpdateExamAsync(exam);
                return Json(new { success = true });
            }
            await PrepareDropdowns(exam);
            return PartialView("_EditModal", exam);
        }
    }
}
