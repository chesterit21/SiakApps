using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-CITY")]
    public class MasterCitiesController : BaseController
    {
        private readonly MasterCityService _masterCityService;
        private readonly MasterProvinceService _masterProvinceService;

        public MasterCitiesController(MasterCityService masterCityService, MasterProvinceService masterProvinceService)
        {
            _masterCityService = masterCityService;
            _masterProvinceService = masterProvinceService;
        }

        // GET: MasterCities
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var cities = await _masterCityService.GetAllAsync();
            return View(cities);
        }

        // GET: MasterCities/Details/5
        [MenuActionAuthorize("VIEW")]
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
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Create()
        {
            var provinces = await _masterProvinceService.GetAllAsync();
            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama");
            return PartialView("_CreateModal");
        }

        // POST: MasterCities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
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
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int id)
        {
            var city = await _masterCityService.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            var provinces = await _masterProvinceService.GetAllAsync();
            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama", city.ProvinsiId);
            return PartialView("_EditModal", city);
        }

        // POST: MasterCities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
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
        [MenuActionAuthorize("DELETE")]
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