
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Contract_Monthly_Claim_System_POE.Models;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // Login GET
    public IActionResult Login()
    {
        return View();
    }

    // Login POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("SubmitClaim", "Claims");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
        }
        return View(model);
    }

    // Logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
