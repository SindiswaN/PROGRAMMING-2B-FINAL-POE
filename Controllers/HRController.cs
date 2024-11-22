using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using Contract_Monthly_Claim_System_POE.Models;

public class HRController : Controller
{
    private readonly AppDbContext _context;

    public HRController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /HR/Dashboard
    public IActionResult Dashboard()
    {
        return View();
    }

    // GET: /HR/ManageLecturers
    public IActionResult ManageLecturers()
    {
        var lecturers = _context.Lecturers.ToList();
        return View(lecturers);
    }

    // GET: /HR/AddLecturer
    [HttpGet]
    public IActionResult AddLecturer()
    {
        return View();
    }

    // POST: /HR/AddLecturer
    [HttpPost]
    public IActionResult AddLecturer(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Add(lecturer);
            _context.SaveChanges();
            return RedirectToAction("ManageLecturers");
        }
        return View(lecturer);
    }

    // GET: /HR/EditLecturer/{id}
    [HttpGet]
    public IActionResult EditLecturer(int id)
    {
        var lecturer = _context.Lecturers.Find(id);
        if (lecturer == null)
        {
            return NotFound();
        }
        return View(lecturer);
    }

    // POST: /HR/EditLecturer
    [HttpPost]
    public IActionResult EditLecturer(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Update(lecturer);
            _context.SaveChanges();
            return RedirectToAction("ManageLecturers");
        }
        return View(lecturer);
    }

    // POST: /HR/DeleteLecturer/{id}
    [HttpPost]
    public IActionResult DeleteLecturer(int id)
    {
        var lecturer = _context.Lecturers.Find(id);
        if (lecturer != null)
        {
            _context.Lecturers.Remove(lecturer);
            _context.SaveChanges();
        }
        return RedirectToAction("ManageLecturers");
    }

    // POST: /HR/GenerateReport
    [HttpPost]
    public IActionResult GenerateReport()
    {
        var approvedClaims = _context.Claims
                                     .Include(c => c.Lecturer)
                                     .Where(c => c.Status == "Approved")
                                     .ToList();

        return View("Report", approvedClaims);
    }

    // POST: /HR/GenerateInvoice
    [HttpPost]
    public IActionResult GenerateInvoice()
    {
        // Set EPPlus license context to non-commercial
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Get approved claims
        var approvedClaims = _context.Claims
                                     .Include(c => c.Lecturer)
                                     .Where(c => c.Status == "Approved")
                                     .ToList();

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Invoice");

            // Add headers
            worksheet.Cells[1, 1].Value = "Claim ID";
            worksheet.Cells[1, 2].Value = "Lecturer Name";
            worksheet.Cells[1, 3].Value = "Date";
            worksheet.Cells[1, 4].Value = "Hours Worked";
            worksheet.Cells[1, 5].Value = "Hourly Rate";
            worksheet.Cells[1, 6].Value = "Total Amount";

            worksheet.Row(1).Style.Font.Bold = true;

            // Add data rows
            int row = 2;
            foreach (var claim in approvedClaims)
            {
                worksheet.Cells[row, 1].Value = claim.ClaimID;
                worksheet.Cells[row, 2].Value = $"{claim.Lecturer.FirstName} {claim.Lecturer.LastName}";
                worksheet.Cells[row, 3].Value = claim.Date.ToShortDateString();
                worksheet.Cells[row, 4].Value = claim.HoursWorked;
                worksheet.Cells[row, 5].Value = claim.HourlyRate;
                worksheet.Cells[row, 6].Value = claim.TotalAmount;
                row++;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            // Generate Excel file
            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Invoice.xlsx");
        }
    }

}

