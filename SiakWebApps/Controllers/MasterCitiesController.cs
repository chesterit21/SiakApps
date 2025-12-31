using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MasterCitiesController : Controller
    {
        private readonly MasterCityService _masterCityService;

        public MasterCitiesController(MasterCityService masterCityService)
        {
            _masterCityService = masterCityService;
        }

        // GET: MasterCities
        public async Task<IActionResult> Index()
        {
            var cities = await _masterCityService.GetAllAsync();
            return View(cities);
        }

        // GET: MasterCities/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var city = await _masterCityService.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // GET: MasterCities/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterCities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvinsiId,Nama")] MasterCity city)
        {
            if (ModelState.IsValid)
            {
                await _masterCityService.CreateAsync(city);
                return Json(new { success = true, message = "City created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterCities/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _masterCityService.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", city);
        }

        // POST: MasterCities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProvinsiId,Nama,CreatedAt,UpdatedAt")] MasterCity city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterCityService.UpdateAsync(city);
                return Json(new { success = true, message = "City updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterCities/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterCityService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "City deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting city" });
        }
    }
}