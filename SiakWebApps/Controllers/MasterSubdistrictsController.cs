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
    [MenuAuthorize("MSTR-SUBDISTRICT")]

    public class MasterSubdistrictsController : BaseController
    {
        private readonly MasterSubdistrictService _masterSubdistrictService;
        private readonly MasterDistrictService _masterDistrictService;
        private readonly MasterCityService _masterCityService;
        private readonly MasterProvinceService _masterProvinceService;

        public MasterSubdistrictsController(
            MasterSubdistrictService masterSubdistrictService, 
            MasterDistrictService masterDistrictService, 
            MasterCityService masterCityService, 
            MasterProvinceService masterProvinceService)
        {
            _masterSubdistrictService = masterSubdistrictService;
            _masterDistrictService = masterDistrictService;
            _masterCityService = masterCityService;
            _masterProvinceService = masterProvinceService;
        }

        // GET: MasterSubdistricts
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var subdistricts = await _masterSubdistrictService.GetAllAsync();
            return View(subdistricts);
        }

        // GET: /MasterSubdistricts/GetCitiesByProvince/5
        [HttpGet]
        [MenuActionAuthorize("VIEW")]
        public async Task<JsonResult> GetCitiesByProvince(int id)
        {
            var cities = await _masterCityService.GetByProvinceIdAsync(id);
            return Json(new SelectList(cities, "Id", "Nama"));
        }

        // GET: /MasterSubdistricts/GetDistrictsByCity/5
        [HttpGet]
        [MenuActionAuthorize("VIEW")]
        public async Task<JsonResult> GetDistrictsByCity(int id)
        {
            var districts = await _masterDistrictService.GetByCityIdAsync(id);
            return Json(new SelectList(districts, "Id", "Nama"));
        }


        // GET: MasterSubdistricts/Details/5
        [MenuActionAuthorize("VIEW")]
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
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create()
        {
            var provinces = await _masterProvinceService.GetAllAsync();
            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama");
            return PartialView("_CreateModal");
        }

        // POST: MasterSubdistricts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("KecamatanId,Nama")] MasterSubdistrict subdistrict)
        {
            if (ModelState.IsValid)
            {
                await _masterSubdistrictService.CreateAsync(subdistrict);
                return Json(new { success = true, message = "Subdistrict created successfully" });
            }

            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterSubdistricts/Edit/5
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id)
        {
            var subdistrict = await _masterSubdistrictService.GetByIdAsync(id);
            if (subdistrict == null)
            {
                return NotFound();
            }

            var district = await _masterDistrictService.GetByIdAsync(subdistrict.KecamatanId);
            var city = await _masterCityService.GetByIdAsync(district?.KotaId ?? 0);
            var provinceId = city?.ProvinsiId ?? 0;
            var cityId = district?.KotaId ?? 0;

            var provinces = await _masterProvinceService.GetAllAsync();
            var cities = await _masterCityService.GetByProvinceIdAsync(provinceId);
            var districts = await _masterDistrictService.GetByCityIdAsync(cityId);

            ViewData["Provinces"] = new SelectList(provinces, "Id", "Nama", provinceId);
            ViewData["Cities"] = new SelectList(cities, "Id", "Nama", cityId);
            ViewData["Districts"] = new SelectList(districts, "Id", "Nama", subdistrict.KecamatanId);
            
            return PartialView("_EditModal", subdistrict);
        }

        // POST: MasterSubdistricts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
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

            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterSubdistricts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("DELETE")]
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