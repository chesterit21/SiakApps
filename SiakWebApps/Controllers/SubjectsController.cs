using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly SubjectService _subjectService;

        public SubjectsController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return View(subjects);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var subject = await _subjectService.GetSubjectByIdAsync(id.Value);
            if (subject == null) return NotFound();
            return PartialView("Details", subject);
        }

        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Kode,TingkatMin,TingkatMax,Deskripsi")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.CreateSubjectAsync(subject);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", subject);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var subject = await _subjectService.GetSubjectByIdAsync(id.Value);
            if (subject == null) return NotFound();
            return PartialView("_EditModal", subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,Kode,TingkatMin,TingkatMax,Deskripsi,CreatedAt")] Subject subject)
        {
            if (id != subject.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _subjectService.UpdateSubjectAsync(subject);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Json(new { success = true });
        }
    }
}
