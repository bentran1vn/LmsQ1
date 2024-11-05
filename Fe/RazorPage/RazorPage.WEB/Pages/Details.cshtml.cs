using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.API;
using RazorPage.WEB.Model;

namespace RazorPage.WEB.Pages;

public class DetailsModel : PageModel
{
    private readonly ISilverJewelryService _service;

    public DetailsModel(ISilverJewelryService service)
    {
        _service = service;
    }

    public SilverJewelry Jewelry { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Jewelry = await _service.GetByIdAsync(id);

        if (Jewelry == null)
        {
            return NotFound();
        }

        return Page();
    }
}