using RazorPage.WEB.Model;

namespace RazorPage.WEB.API;

public interface ISilverJewelryService
{
    Task<IEnumerable<SilverJewelry>> GetAllAsync(string searchTerm = null);
    Task<SilverJewelry> GetByIdAsync(string id);
    Task<bool> CreateAsync(SilverJewelry jewelry);
    Task<bool> UpdateAsync(SilverJewelry jewelry);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
}