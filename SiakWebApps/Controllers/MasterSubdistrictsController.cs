using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MasterSubdistrictsController : Controller
    {
        private readonly MasterSubdistrictService _masterSubdistrictService;

        public MasterSubdistrictsController(MasterSubdistrictService masterSubdistrictService)
        {
            _masterSubdistrictService = masterSubdistrictService;
        }

        // GET: MasterSubdistricts
        public async Task<IActionResult> Index()
        {
            var subdistricts = await _masterSubdistrictService.GetAllAsync();
            return View(subdistricts);
        }

        // GET: MasterSubdistricts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subdistrict = await _masterSubdistrictService.GetByIdAsync(id);
            if (subdistrict == null)
            {
                return NotFound();
            }
            return View(subdistrict);
        }

        // GET: MasterSubdistricts/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterSubdistricts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KecamatanId,Nama")] MasterSubdistrict subdistrict)
        {
            if (ModelState.IsValid)
            {
                await _masterSubdistrictService.CreateAsync(subdistrict);
                return Json(new { success = true, message = "Subdistrict created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterSubdistricts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subdistrict = await _masterSubdistrictService.GetByIdAsync(id);
            if (subdistrict == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", subdistrict);
        }

        // POST: MasterSubdistricts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KecamatanId,Nama,CreatedAt,UpdatedAt")] MasterSubdistrict subdistrict)
        {
            if (id != subdistrict.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterSubdistrictService.UpdateAsync(subdistrict);
                return Json(new { success = true, message = "Subdistrict updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterSubdistricts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterSubdistrictService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Subdistrict deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting subdistrict" });
        }
    }
}