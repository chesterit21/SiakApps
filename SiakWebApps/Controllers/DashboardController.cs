using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using Microsoft.AspNetCore.Authorization;

namespace SiakWebApps.Controllers;


[MenuAuthorize("DASHBOARD")]

public class DashboardController : BaseController
{
    [MenuActionAuthorize("VIEW")]
    public IActionResult Index()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}