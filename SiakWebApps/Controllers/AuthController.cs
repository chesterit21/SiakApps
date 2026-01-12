using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Security.Claims;
using System.Text.Json;

namespace SiakWebApps.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly UserRoleService _userRoleService;
        private readonly MenuAppService _menuAppService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserService userService, UserRoleService userRoleService, MenuAppService menuAppService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _menuAppService = menuAppService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            _logger.LogInformation("AuthController Login GET action called");
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _logger.LogInformation("AuthController Login POST action called");
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByUsernameAsync(loginDto.Username);
                _logger.LogInformation("AuthController ModelState.IsValid");
                _logger.LogInformation("User from DB {0}", user.Username);
                _logger.LogInformation("AuthController {0}", loginDto.Password);
                _logger.LogInformation("Pass from DB {0}", user.Password);

                if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    // Get user role
                    var userRole = await _userRoleService.GetRoleByUserIdAsync(user.Id);
                    var roleId = userRole?.RoleId ?? 0; // Default to 0 if no role
                    var roleName = userRole?.Role?.Name ?? "Guest";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email ?? ""),
                        new Claim(ClaimTypes.Role, roleName)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    _logger.LogInformation("User {Username} logged in successfully.", user.Username);

                    // Fetch and store menu in session
                    if (roleId > 0)
                    {
                        var menu = await _menuAppService.GetByRoleIdAsync(roleId);
                        var menuJson = JsonSerializer.Serialize(menu);
                        HttpContext.Session.SetString("UserMenu", menuJson);
                        _logger.LogInformation("Menu for role {RoleId} stored in session.", roleId);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
                
                _logger.LogWarning("Invalid login attempt for username {Username}", loginDto.Username);
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(loginDto);
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User {Username} logging out.", User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}