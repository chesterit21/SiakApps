using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MasterDistrictsController : Controller
    {
        private readonly MasterDistrictService _masterDistrictService;

        public MasterDistrictsController(MasterDistrictService masterDistrictService)
        {
            _masterDistrictService = masterDistrictService;
        }

        // GET: MasterDistricts
        public async Task<IActionResult> Index()
        {
            var districts = await _masterDistrictService.GetAllAsync();
            return View(districts);
        }

        // GET: MasterDistricts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var district = await _masterDistrictService.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return View(district);
        }

        // GET: MasterDistricts/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterDistricts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KotaId,Nama")] MasterDistrict district)
        {
            if (ModelState.IsValid)
            {
                await _masterDistrictService.CreateAsync(district);
                return Json(new { success = true, message = "District created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterDistricts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var district = await _masterDistrictService.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", district);
        }

        // POST: MasterDistricts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KotaId,Nama,CreatedAt,UpdatedAt")] MasterDistrict district)
        {
            if (id != district.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterDistrictService.UpdateAsync(district);
                return Json(new { success = true, message = "District updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterDistricts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterDistrictService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "District deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting district" });
        }
    }
}