using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.WEB.API;
using RazorPage.WEB.Model;

namespace RazorPage.WEB.Pages;

public class IndexModel : PageModel
{
    private readonly ISilverJewelryService _service;

    public IndexModel(ISilverJewelryService service)
    {
        _service = service;
    }

    public IEnumerable<SilverJewelry> Jewelries { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        Jewelries = await _service.GetAllAsync(SearchTerm);
    }
}