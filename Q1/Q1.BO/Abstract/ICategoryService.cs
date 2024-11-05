using Q1.BO.Services.Category;
using Q1.DAO.Models;

namespace Q1.BO.Abstract;

public interface ICategoryService
{
    Task<List<Response.Category>> GetAllCate();
}