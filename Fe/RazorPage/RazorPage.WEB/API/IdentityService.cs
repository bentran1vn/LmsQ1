using Microsoft.AspNetCore.Identity.Data;

namespace RazorPage.WEB.API;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private const string LoginUrl = "http://localhost:8080/Identity/login";
    private string _token;

    public IdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginUrl, request);
        if (response.IsSuccessStatusCode)
        {
            _token = await response.Content.ReadAsStringAsync();
            return _token;
        }
        else
        {
            throw new Exception("Login failed.");
        }
    }
    
    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(_token);
    }
    
    public string GetToken()
    {
        return _token;
    }
}