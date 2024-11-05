using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.API;
using RazorPage.WEB.Model;

namespace RazorPage.WEB.Pages;

public class EditModel : PageModel
{
    private readonly ISilverJewelryService _service;

    public EditModel(ISilverJewelryService service)
    {
        _service = service;
    }

    [BindProperty]
    public SilverJewelry Jewelry { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Jewelry = await _service.GetByIdAsync(id);
        Categories = await _service.GetCategoriesAsync();

        if (Jewelry == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            Console.WriteLine(Jewelry.SilverJewelryId);
            await _service.UpdateAsync(Jewelry);
            return RedirectToPage("./Index");
        }
        catch
        {
            ModelState.AddModelError("", "Error updating the jewelry. Please try again.");
            return Page();
        }
    }
}
