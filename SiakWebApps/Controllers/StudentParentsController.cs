using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;
using SiakWebApps.DataAccess;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    public class StudentParentsController : BaseController
    {
        private readonly StudentParentService _studentParentService;
        private readonly StudentRepository _studentRepository;
        private readonly ParentRepository _parentRepository;

        public StudentParentsController(StudentParentService studentParentService, StudentRepository studentRepository, ParentRepository parentRepository)
        {
            _studentParentService = studentParentService;
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _studentParentService.GetAllAsync();
            return Json(new { data });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Json(students);
        }

        [HttpGet]
        public async Task<IActionResult> GetParents()
        {
            var parents = await _parentRepository.GetAllAsync();
            return Json(parents);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int siswaId, int orangTuaId)
        {
            var studentParent = await _studentParentService.GetByIdAsync(siswaId, orangTuaId);
            if (studentParent == null)
            {
                return NotFound();
            }
            return Json(studentParent);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentParent studentParent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentParentService.CreateAsync(studentParent);
            if (!result)
            {
                return BadRequest(new { message = "Failed to create student-parent relationship." });
            }
            return Ok(new { message = "Student-parent relationship created successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StudentParent studentParent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentParentService.UpdateAsync(studentParent);
            if (!result)
            {
                return NotFound(new { message = "Student-parent relationship not found or failed to update." });
            }
            return Ok(new { message = "Student-parent relationship updated successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int siswaId, int orangTuaId)
        {
            var result = await _studentParentService.DeleteAsync(siswaId, orangTuaId);
            if (!result)
            {
                return NotFound(new { message = "Student-parent relationship not found or failed to delete." });
            }
            return Ok(new { message = "Student-parent relationship deleted successfully." });
        }
    }
}
