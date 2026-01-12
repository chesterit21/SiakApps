using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;

namespace SiakWebApps.Controllers;
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("HomeController Index action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        var passwordToHash = "password123";
        var newHash = BCrypt.Net.BCrypt.HashPassword(passwordToHash);
        _logger.LogInformation("--- PASSWORD HASH GENERATOR ---");
        _logger.LogInformation($"Password to Hash: {passwordToHash}");
        _logger.LogInformation($"Generated BCrypt Hash: {newHash}");
        _logger.LogInformation("-----------------------------");
        _logger.LogInformation("Salin hash di atas dan update database Anda. Hapus kode ini dari Program.cs setelah selesai.");

        return View();
    }

    public IActionResult ContactUs()
    {
        _logger.LogInformation("HomeController ContactUs action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }
    public IActionResult Faq()
    {
        _logger.LogInformation("HomeController Faq action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }
    public IActionResult News()
    {
        _logger.LogInformation("HomeController News action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult AboutUs()
    {
        _logger.LogInformation("HomeController AboutUs action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Gallery()
    {
        _logger.LogInformation("HomeController Gallery action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult AcademicCalendar()
    {
        _logger.LogInformation("HomeController AcademicCalendar action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Facilities()
    {
        _logger.LogInformation("HomeController Facilities action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Extracurricular()
    {
        _logger.LogInformation("HomeController Extracurricular action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Admissions()
    {
        _logger.LogInformation("HomeController Admissions action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Profile()
    {
        _logger.LogInformation("HomeController Profile action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("HomeController Privacy action called");
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogInformation("HomeController Error action called");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
