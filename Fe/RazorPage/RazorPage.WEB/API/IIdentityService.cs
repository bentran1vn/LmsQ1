using Microsoft.AspNetCore.Identity.Data;

namespace RazorPage.WEB.API;

public interface IIdentityService
{
    Task<string> LoginAsync(LoginRequest request);
    // void Logout();
    bool IsLoggedIn();
    string GetToken();
}