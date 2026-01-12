using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("FIN-TRANS")]

    public class PaymentTransactionController : BaseController
    {
        private readonly PaymentTransactionService _transactionService;
        private readonly StudentService _studentService;
        private readonly AcademicYearService _academicYearService;
        private readonly SemesterService _semesterService;

        public PaymentTransactionController(
            PaymentTransactionService transactionService,
            StudentService studentService,
            AcademicYearService academicYearService,
            SemesterService semesterService)
        {
            _transactionService = transactionService;
            _studentService = studentService;
            _academicYearService = academicYearService;
            _semesterService = semesterService;
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllAsync();
            return View(transactions);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var transaction = await _transactionService.GetByIdAsync(id.Value);
            if (transaction == null) return NotFound();
            return PartialView("Details", transaction);
        }

        private async Task PrepareDropdowns(PaymentTransaction? transaction = null)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var academicYears = await _academicYearService.GetAllAcademicYearsAsync();
            var semesters = await _semesterService.GetAllSemestersAsync();

            ViewBag.SiswaId = new SelectList(students, "Id", "NamaLengkap", transaction?.SiswaId);
            ViewBag.TahunAjaranId = new SelectList(academicYears, "Id", "Nama", transaction?.TahunAjaranId);
            ViewBag.SemesterId = new SelectList(semesters, "Id", "Nama", transaction?.SemesterId);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Create()
        {
            await PrepareDropdowns();
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("SiswaId,TahunAjaranId,SemesterId,JenisPembayaran,Jumlah,TanggalPembayaran,BuktiPembayaran,Status")] PaymentTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.CreatedBy = 1; // Mock user ID
                await _transactionService.CreateAsync(transaction);
                return Json(new { success = true });
            }
            await PrepareDropdowns(transaction);
            return PartialView("_CreateModal", transaction);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var transaction = await _transactionService.GetByIdAsync(id.Value);
            if (transaction == null) return NotFound();
            await PrepareDropdowns(transaction);
            return PartialView("_EditModal", transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiswaId,TahunAjaranId,SemesterId,JenisPembayaran,Jumlah,TanggalPembayaran,BuktiPembayaran,Status,CreatedBy,CreatedAt")] PaymentTransaction transaction)
        {
            if (id != transaction.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _transactionService.UpdateAsync(transaction);
                return Json(new { success = true });
            }
            await PrepareDropdowns(transaction);
            return PartialView("_EditModal", transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
