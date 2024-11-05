using Microsoft.AspNetCore.Mvc;
using Q1.BO.Abstract;
using Q1.BO.Services.SilverJewelry;

namespace Q1.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SilverJewelryController : ControllerBase
{
    private readonly ISilverJewelryService _silverJewelryService;

    public SilverJewelryController(ISilverJewelryService silverJewelryService)
    {
        _silverJewelryService = silverJewelryService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateSil([FromBody] Request.Create request)
    {
        try
        {
            await _silverJewelryService.CreateSJ(request);
            return Ok("Create Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut()]
    public async Task<IActionResult> UpdateSil([FromBody] Request.Create request)
    {
        try
        {
            await _silverJewelryService.UpdateSJ(request);
            return Ok("Update Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{silverJewelryId}")]
    public async Task<IActionResult> UpdateSil(string silverJewelryId)
    {
        try
        {
            await _silverJewelryService.DeleteSJ(silverJewelryId);
            return Ok("Delete Successfully !");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetSil(string? searchTerm = null)
    {
        try
        {
            var result = await _silverJewelryService.GetAll(searchTerm);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}