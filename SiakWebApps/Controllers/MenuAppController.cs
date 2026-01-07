using Microsoft.AspNetCore.Mvc;
using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAppController : ControllerBase
    {
        private readonly MenuAppService _menuAppService;

        public MenuAppController(MenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuApp>>> GetAll()
        {
            var menuApps = await _menuAppService.GetAllAsync();
            return Ok(menuApps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuApp>> GetById(int id)
        {
            var menuApp = await _menuAppService.GetByIdAsync(id);
            
            if (menuApp == null)
            {
                return NotFound();
            }

            return Ok(menuApp);
        }

        [HttpPost]
        public async Task<ActionResult<MenuApp>> Create(MenuApp menuApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _menuAppService.CreateAsync(menuApp);
            menuApp.Id = id;

            return CreatedAtAction(nameof(GetById), new { id = menuApp.Id }, menuApp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuApp menuApp)
        {
            if (!ModelState.IsValid || id != menuApp.Id)
            {
                return BadRequest(ModelState);
            }

            var updated = await _menuAppService.UpdateAsync(menuApp);
            if (!updated)
            {
                return NotFound();
            }

            return Ok(menuApp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _menuAppService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok(new { message = "Menu deleted successfully" });
        }
    }
}