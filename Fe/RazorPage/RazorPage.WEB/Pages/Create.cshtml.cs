using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.API;
using RazorPage.WEB.Model;

namespace RazorPage.WEB.Pages;

public class CreateModel : PageModel
{
    private readonly ISilverJewelryService _service;

    public CreateModel(ISilverJewelryService service)
    {
        _service = service;
    }

    [BindProperty]
    public SilverJewelry Jewelry { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    public async Task OnGetAsync()
    {
        Categories = await _service.GetCategoriesAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Jewelry.CreatedDate = DateTime.UtcNow;
        await _service.CreateAsync(Jewelry);
        return RedirectToPage("./Index");
    }
}