using System.Net.Http.Headers;
using RazorPage.WEB.Model;

namespace RazorPage.WEB.API;

public class SilverJewelryService : ISilverJewelryService
{
    private readonly HttpClient _httpClient;
    private readonly IIdentityService _identityService;
    private const string BaseUrl = "http://localhost:8080/SilverJewelry";
    private const string CategoryUrl = "http://localhost:8080/Category";

    public SilverJewelryService(HttpClient httpClient, IIdentityService identityService)
    {
        _httpClient = httpClient;
        _identityService = identityService;
    }
    
    private void AddAuthorizationHeader()
        {
            if (_identityService.IsLoggedIn())
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _identityService.GetToken());
            }
        }

    public async Task<IEnumerable<SilverJewelry>> GetAllAsync(string searchTerm = null)
    {
        var url = string.IsNullOrEmpty(searchTerm) ? BaseUrl : $"{BaseUrl}?searchTerm={searchTerm}";
        return await _httpClient.GetFromJsonAsync<IEnumerable<SilverJewelry>>(url);
    }

    public async Task<SilverJewelry> GetByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<SilverJewelry>($"{BaseUrl}/{id}");
    }

    public async Task<bool> CreateAsync(SilverJewelry jewelry)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, jewelry);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(SilverJewelry jewelry)
    {
        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync(BaseUrl, jewelry);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        
    }

    public async Task<bool> DeleteAsync(string id)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
    
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Category>>(CategoryUrl);
    }
}