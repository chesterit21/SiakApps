using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseService _expenseService;
        private readonly AcademicYearService _academicYearService;
        private readonly SemesterService _semesterService;
        private readonly MasterBudgetCategoryService _budgetCategoryService;

        public ExpenseController(
            ExpenseService expenseService,
            AcademicYearService academicYearService,
            SemesterService semesterService,
            MasterBudgetCategoryService budgetCategoryService)
        {
            _expenseService = expenseService;
            _academicYearService = academicYearService;
            _semesterService = semesterService;
            _budgetCategoryService = budgetCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _expenseService.GetAllAsync();
            return View(expenses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var expense = await _expenseService.GetByIdAsync(id.Value);
            if (expense == null) return NotFound();
            return PartialView("Details", expense);
        }

        private async Task PrepareDropdowns(Expense? expense = null)
        {
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            var semesters = await _semesterService.GetAllSemestersAsync();
            var budgetCategories = await _budgetCategoryService.GetAllAsync();

            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", expense?.TahunAjaranId);
            ViewBag.SemesterId = new SelectList(semesters, "Id", "Nama", expense?.SemesterId);
            ViewBag.AnggaranId = new SelectList(budgetCategories, "Id", "Nama", expense?.AnggaranId);
        }

        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnggaranId,TahunAjaranId,SemesterId,NamaPengeluaran,Jumlah,Deskripsi,TanggalPengeluaran,BuktiPengeluaran")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                // Set CreatedBy to a mock user ID for now
                expense.CreatedBy = 1;
                await _expenseService.CreateAsync(expense);
                return Json(new { success = true });
            }
            await PrepareDropdowns(expense);
            return PartialView("_CreateModal", expense);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var expense = await _expenseService.GetByIdAsync(id.Value);
            if (expense == null) return NotFound();
            await PrepareDropdowns(expense);
            return PartialView("_EditModal", expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnggaranId,TahunAjaranId,SemesterId,NamaPengeluaran,Jumlah,Deskripsi,TanggalPengeluaran,BuktiPengeluaran,CreatedBy,CreatedAt")] Expense expense)
        {
            if (id != expense.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateAsync(expense);
                return Json(new { success = true });
            }
            await PrepareDropdowns(expense);
            return PartialView("_EditModal", expense);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expenseService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
