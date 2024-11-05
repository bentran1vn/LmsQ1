using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.API;

namespace RazorPage.WEB.Pages;

public class LoginModel : PageModel
{
    private readonly IIdentityService _identityService;

    public LoginModel(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [BindProperty]
    public LoginRequest LoginRequest { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var token = await _identityService.LoginAsync(LoginRequest);
            HttpContext.Session.SetString("token", token);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return Page();
        }
    }
}