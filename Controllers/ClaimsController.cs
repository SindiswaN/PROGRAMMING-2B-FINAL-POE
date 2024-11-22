using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Contract_Monthly_Claim_System_POE.Models;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ClaimsController : Controller
{
    private readonly AppDbContext _context;

    public ClaimsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Claims/SubmitClaim
    [HttpGet]
    public IActionResult SubmitClaim()
    {
        if (HttpContext.Session.GetInt32("LecturerID") == null)
        {
            return RedirectToAction("Login", "Lecturers");
        }

        return View();
    }

    // POST: /Claims/SubmitClaim
    [HttpPost]
    public IActionResult SubmitClaim(int hoursWorked, decimal hourlyRate, string notes, IFormFile supportingDoc)
    {
        try
        {
            var lecturerID = HttpContext.Session.GetInt32("LecturerID");
            if (lecturerID == null)
            {
                return RedirectToAction("Login", "Lecturers");
            }

            if (hoursWorked <= 0 || hourlyRate <= 0)
            {
                ViewBag.ErrorMessage = "Hours worked and hourly rate must be positive numbers.";
                return View();
            }

            if (supportingDoc == null)
            {
                ViewBag.ErrorMessage = "A supporting document is required.";
                return View();
            }

            string filePath = null;
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            filePath = Path.Combine(uploads, supportingDoc.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                supportingDoc.CopyTo(stream);
            }

            var claim = new Claim
            {
                LecturerID = (int)lecturerID,
                Date = DateTime.Now,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                TotalAmount = hoursWorked * hourlyRate,
                Notes = notes,
                Status = "Pending",
                SupportingDocumentPath = filePath
            };

            _context.Claims.Add(claim);
            _context.SaveChanges();

            return RedirectToAction("ClaimSubmitted");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View();
        }
    }

    // GET: /Claims/ClaimSubmitted
    public IActionResult ClaimSubmitted()
    {
        return View();
    }

    // GET: /Claims/ListClaims
    public IActionResult ListClaims()
    {
        var claims = _context.Claims
                             .Include(c => c.Lecturer) // Ensure lecturer data is loaded
                             .ToList();

        return View(claims);
    }

    // GET: /Claims/VerifyClaims
    public IActionResult VerifyClaims()
    {
        var role = HttpContext.Session.GetString("Role");
        if (role != "Coordinator" && role != "Manager")
        {
            return RedirectToAction("Login", "Lecturers");
        }

        var pendingClaims = _context.Claims
                                    .Include(c => c.Lecturer)
                                    .Where(c => c.Status == "Pending")
                                    .ToList();

        foreach (var claim in pendingClaims)
        {
            if (claim.HourlyRate > 100)
            {
                claim.Notes += " [Flagged: High hourly rate]";
            }

            if (claim.HoursWorked > 40)
            {
                claim.Notes += " [Flagged: Exceeds weekly hours]";
            }
        }

        return View(pendingClaims);
    }

    // POST: /Claims/VerifyClaim
    [HttpPost]
    public IActionResult VerifyClaim(int claimID, string action, string? rejectionReason)
    {
        var claim = _context.Claims.Find(claimID);
        if (claim == null)
        {
            return NotFound();
        }

        if (action == "Approve")
        {
            claim.Status = "Approved";
            claim.RejectionReason = null; // Clear any previous rejection reason
        }
        else if (action == "Reject")
        {
            if (string.IsNullOrEmpty(rejectionReason))
            {
                TempData["ErrorMessage"] = "Rejection reason is required when rejecting a claim.";
                return RedirectToAction("VerifyClaims");
            }

            claim.Status = "Rejected";
            claim.RejectionReason = rejectionReason;
        }

        _context.SaveChanges();
        return RedirectToAction("VerifyClaims");
    }
}
