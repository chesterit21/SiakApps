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
    [MenuAuthorize("MSTR-DISTRICT")]

    public class MasterDistrictsController : BaseController
    {
        private readonly MasterDistrictService _masterDistrictService;
        private readonly MasterCityService _masterCityService;
        private readonly MasterProvinceService _masterProvinceService;

        public MasterDistrictsController(MasterDistrictService masterDistrictService, MasterCityService masterCityService, MasterProvinceService masterProvinceService)
        {
            _masterDistrictService = masterDistrictService;
            _masterCityService = masterCityService;
            _masterProvinceService = masterProvinceService;
        }

        // GET: MasterDistricts
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var districts = await _masterDistrictService.GetAllAsync();
            return View(districts);
        }

        // GET: /MasterDistricts/GetCitiesByProvince/5
        [HttpGet]
        [MenuActionAuthorize("VIEW")]
        public async Task<JsonResult> GetCitiesByProvince(int id)
        {
            var cities = await _masterCityService.GetByProvinceIdAsync(id);
            return Json(new SelectList(cities, "Id", "Nama"));
        }

        // GET: MasterDistricts/Details/5
        [MenuActionAuthorize("VIEW")]
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
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Create()
        {
            var provinces = await _masterProvinceService.GetAllAsync();
            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama");
            return PartialView("_CreateModal");
        }

        // POST: MasterDistricts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
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
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int id)
        {
            var district = await _masterDistrictService.GetByIdAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            
            var city = await _masterCityService.GetByIdAsync(district.KotaId);
            var provinceId = city?.ProvinsiId ?? 0;

            var provinces = await _masterProvinceService.GetAllAsync();
            var cities = await _masterCityService.GetByProvinceIdAsync(provinceId);

            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama", provinceId);
            ViewData["Cities"] = new SelectList(cities, "Id", "Nama", district.KotaId);

            return PartialView("_EditModal", district);
        }

        // POST: MasterDistricts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
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
        [MenuActionAuthorize("DELETE")]
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