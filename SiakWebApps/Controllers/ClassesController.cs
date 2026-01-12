using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-CLASS")]
    public class ClassesController : BaseController
    {
        private readonly ClassService _classService;

        public ClassesController(ClassService classService)
        {
            _classService = classService;
        }

        // GET: Classes
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var classes = await _classService.GetAllClassesAsync();
            return View(classes);
        }

        // GET: Classes/Details/5
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int id)
        {
            var classModel = await _classService.GetClassByIdAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }
            return View(classModel);
        }

        // GET: Classes/Create
        [MenuActionAuthorize("ADD")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: Classes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("Nama,Tingkat,TahunAjaranId,WaliKelasId,Kapasitas,CreatedAt,UpdatedAt")] Class classModel)
        {
            if (ModelState.IsValid)
            {
                await _classService.CreateClassAsync(classModel);
                return Json(new { success = true, message = "Class created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var classModel = await _classService.GetClassByIdAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", classModel);
        }

        // POST: Classes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,Tingkat,TahunAjaranId,WaliKelasId,Kapasitas,CreatedAt,UpdatedAt")] Class classModel)
        {
            if (id != classModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _classService.UpdateClassAsync(classModel);
                return Json(new { success = true, message = "Class updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: Classes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> Delete(int id)
        {
            // Karena tidak ada metode Delete di service, kita lewati dulu
            return Json(new { success = false, message = "Delete not implemented" });
        }
    }
}