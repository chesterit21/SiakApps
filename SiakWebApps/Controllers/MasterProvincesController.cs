using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-PROVINCE")]
    public class MasterProvincesController : BaseController
    {
        private readonly MasterProvinceService _masterProvinceService;

        public MasterProvincesController(MasterProvinceService masterProvinceService)
        {
            _masterProvinceService = masterProvinceService;
        }

        // GET: MasterProvinces
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var provinces = await _masterProvinceService.GetAllAsync();
            return View(provinces);
        }

        // GET: MasterProvinces/Details/5
        [MenuActionAuthorize("VIEW")]
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
        [MenuActionAuthorize("VIEW")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterProvinces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
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
        [MenuActionAuthorize("VIEW")]
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
        [MenuActionAuthorize("EDIT")]
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
        [MenuActionAuthorize("DELETE")]
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