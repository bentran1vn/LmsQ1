using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Q1.BO.Abstract;
using Q1.BO.Services.Identity;

namespace Q1.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityServices _identityServices;

    public IdentityController(IIdentityServices identityServices)
    {
        _identityServices = identityServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Request.Login request)
    {
        try
        {
            var result = await _identityServices.Login(request.Email, request.Password);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpGet("me")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetMe()
    {
        
        return Ok("kakka");
    }
}