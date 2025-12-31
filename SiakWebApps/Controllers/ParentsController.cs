using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class ParentsController : Controller
    {
        private readonly ParentService _parentService;

        public ParentsController(ParentService parentService)
        {
            _parentService = parentService;
        }

        // GET: Parents
        public async Task<IActionResult> Index()
        {
            var parents = await _parentService.GetAllAsync();
            return View(parents);
        }

        // GET: Parents/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var parent = await _parentService.GetByIdAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: Parents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Nik,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,Agama,Phone,Email,Pekerjaan,PenghasilanPerBulan,AlamatId")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                await _parentService.CreateAsync(parent);
                return Json(new { success = true, message = "Parent created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: Parents/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var parent = await _parentService.GetByIdAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", parent);
        }

        // POST: Parents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Nik,NamaLengkap,JenisKelaminId,TempatLahir,TanggalLahir,Agama,Phone,Email,Pekerjaan,PenghasilanPerBulan,AlamatId,CreatedAt,UpdatedAt,DeletedAt")] Parent parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _parentService.UpdateAsync(parent);
                return Json(new { success = true, message = "Parent updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: Parents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _parentService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Parent deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting parent" });
        }
    }
}