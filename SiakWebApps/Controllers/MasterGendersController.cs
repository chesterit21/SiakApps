using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class MasterGendersController : Controller
    {
        private readonly MasterGenderService _masterGenderService;

        public MasterGendersController(MasterGenderService masterGenderService)
        {
            _masterGenderService = masterGenderService;
        }

        // GET: MasterGenders
        public async Task<IActionResult> Index()
        {
            var genders = await _masterGenderService.GetAllAsync();
            return View(genders);
        }

        // GET: MasterGenders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var gender = await _masterGenderService.GetByIdAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // GET: MasterGenders/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterGenders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama")] MasterGender gender)
        {
            if (ModelState.IsValid)
            {
                await _masterGenderService.CreateAsync(gender);
                return Json(new { success = true, message = "Gender created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterGenders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gender = await _masterGenderService.GetByIdAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", gender);
        }

        // POST: MasterGenders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,CreatedAt,UpdatedAt")] MasterGender gender)
        {
            if (id != gender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterGenderService.UpdateAsync(gender);
                return Json(new { success = true, message = "Gender updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterGenders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterGenderService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Gender deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting gender" });
        }
    }
}