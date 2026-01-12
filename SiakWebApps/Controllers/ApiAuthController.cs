using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SiakWebApps.Services; // Ditambahkan untuk UserService, UserRoleService, MenuAppService

namespace SiakWebApps.Controllers
{
    /// <summary>
    /// Controller untuk autentikasi API dengan endpoint login khusus
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class ApiAuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserRoleService _userRoleService;
        private readonly MenuAppService _menuAppService;
        private readonly ILogger<ApiAuthController> _logger;

        public ApiAuthController(UserService userService, 
                                UserRoleService userRoleService,
                                MenuAppService menuAppService,
                                ILogger<ApiAuthController> logger)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _menuAppService = menuAppService;
            _logger = logger;
        }

        /// <summary>
        /// Autentikasi pengguna melalui API dan mengembalikan respons JSON
        /// </summary>
        /// <param name="loginDto">Kredensial pengguna</param>
        /// <response code="200">Login berhasil - mengembalikan token sesi</response>
        /// <response code="400">Kredensial tidak valid</response>
        /// <response code="500">Kesalahan server internal</response>
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid input format" });
            }

            try
            {
                var user = await _userService.GetUserByUsernameAsync(loginDto.Username);
                
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    _logger.LogWarning("Login gagal untuk pengguna: {Username}", loginDto.Username);
                    return BadRequest(new { error = "Invalid username or password" });
                }

                // Siapkan data sesi (sesuai dengan implementasi existing)
                var userRole = await _userRoleService.GetRoleByUserIdAsync(user.Id);
                var roleId = userRole?.RoleId ?? 0;
                var userMenus = await _menuAppService.GetByRoleIdAsync(roleId);
                
                HttpContext.Session.SetString("UserMenu", JsonSerializer.Serialize(userMenus));

                // Tambahkan custom headers sesuai permintaan
                Response.Headers.Add("SFCORE-APPX", "v2.1.0");
                Response.Headers.Add("SiakX-Apps", "EDU-PLATFORM/2026-Q2");

                return Ok(new 
                {
                    success = true,
                    message = "Authentication successful",
                    token = Guid.NewGuid().ToString(),
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kesalahan saat proses login");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}