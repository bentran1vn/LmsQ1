using Microsoft.AspNetCore.Mvc;
using Q1.BO.Abstract;

namespace Q1.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetCate()
    {
        try
        {
            var result = await _categoryService.GetAllCate();
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}