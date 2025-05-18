using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTutionHub.Models;

namespace SmartTutionHub.Controllers;

public class HomeController : Controller
{
    // Welcome page for unauthenticated users
    [AllowAnonymous]
    public IActionResult Index()
    {
        // If already logged in, send to Dashboard
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction(nameof(Dashboard));

        return View();
    }

    // Dashboard for logged-in users
    [Authorize]
    public IActionResult Dashboard()
    {
        // TODO: load user-specific data (courses, bookings...)
        return View();
    }
}
