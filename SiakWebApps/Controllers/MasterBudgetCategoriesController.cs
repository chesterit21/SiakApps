using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-BUDGETCAT")]
   public class MasterBudgetCategoriesController : BaseController
    {
        private readonly MasterBudgetCategoryService _masterBudgetCategoryService;

        public MasterBudgetCategoriesController(MasterBudgetCategoryService masterBudgetCategoryService)
        {
            _masterBudgetCategoryService = masterBudgetCategoryService;
        }

        // GET: MasterBudgetCategories
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var categories = await _masterBudgetCategoryService.GetAllAsync();
            return View(categories);
        }

        // GET: MasterBudgetCategories/Details/5
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _masterBudgetCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: MasterBudgetCategories/Create
        [MenuActionAuthorize("VIEW")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: MasterBudgetCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("Nama")] MasterBudgetCategory category)
        {
            if (ModelState.IsValid)
            {
                await _masterBudgetCategoryService.CreateAsync(category);
                return Json(new { success = true, message = "Category created successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // GET: MasterBudgetCategories/Edit/5
        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _masterBudgetCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", category);
        }

        // POST: MasterBudgetCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,CreatedAt,UpdatedAt")] MasterBudgetCategory category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _masterBudgetCategoryService.UpdateAsync(category);
                return Json(new { success = true, message = "Category updated successfully" });
            }
            
            // If we got this far, something failed, redisplay form
            var errors = ModelState.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.FirstOrDefault()?.ErrorMessage))
                                   .Where(x => x.Value != null)
                                   .ToDictionary(x => x.Key, x => x.Value);
            return Json(new { success = false, errors });
        }

        // POST: MasterBudgetCategories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _masterBudgetCategoryService.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Category deleted successfully" });
            }
            return Json(new { success = false, message = "Error deleting category" });
        }
    }
}