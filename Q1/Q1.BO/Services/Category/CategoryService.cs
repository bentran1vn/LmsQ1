using Microsoft.EntityFrameworkCore;
using Q1.BO.Abstract;
using Q1.DAO.Abstract;

namespace Q1.BO.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IRepositoryBase<DAO.Models.Category> _categoryRepository;

    public CategoryService(IRepositoryBase<DAO.Models.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Response.Category>> GetAllCate()
    {
        var list = await _categoryRepository.FindAll(null).ToListAsync();
        return list.Select(x => new Response.Category(x.CategoryId, x.CategoryName, x.CategoryDescription, x.FromCountry ?? "VietNam")).ToList();
    }
}