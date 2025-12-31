using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MasterProvincesController : Controller
    {
        private readonly MasterProvinceService _masterProvinceService;

        public MasterProvincesController(MasterProvinceService masterProvinceService)
        {
            _masterProvinceService = masterProvinceService;
        }

        // GET: MasterProvinces
        public async Task<IActionResult> Index()
        {
            var provinces = await _masterProvinceService.GetAllAsync();
            return View(provinces);
        }

        // GET: MasterProvinces/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var province = await _masterProvinceService.GetByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }

        // GET: MasterProvinces/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterProvinces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama")] MasterProvince province)
        {
            if (ModelState.IsValid)
            {
                await _masterProvinceService.CreateAsync(province);
                return Json(new { success = true, message = "Province created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterProvinces/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var province = await _masterProvinceService.GetByIdAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", province);
        }

        // POST: MasterProvinces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,CreatedAt,UpdatedAt")] MasterProvince province)
        {
            if (id != province.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterProvinceService.UpdateAsync(province);
                return Json(new { success = true, message = "Province updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterProvinces/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterProvinceService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Province deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting province" });
        }
    }
}