using Microsoft.AspNetCore.Mvc;
using Contract_Monthly_Claim_System_POE.Models;
using System.Linq;

public class LecturersController : Controller
{
    private readonly AppDbContext _context;

    public LecturersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Check if a lecturer with the provided credentials exists
        var lecturer = _context.Lecturers.SingleOrDefault(l => l.Email == email && l.Password == password);
        if (lecturer != null)
        {
            // Store user ID and role in session
            HttpContext.Session.SetInt32("LecturerID", lecturer.LecturerID);
            HttpContext.Session.SetString("Role", lecturer.Role);

            // Redirect based on role
            if (lecturer.Role == "Coordinator" || lecturer.Role == "Manager")
            {
                return RedirectToAction("VerifyClaims", "Claims");
            }
            else
            {
                return RedirectToAction("SubmitClaim", "Claims");
            }
        }

        // Invalid credentials
        ViewBag.Error = "Invalid email or password.";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
