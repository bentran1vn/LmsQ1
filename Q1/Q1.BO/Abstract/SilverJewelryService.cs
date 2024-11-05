using Q1.BO.Services.SilverJewelry;
using Q1.DAO.Models;

namespace Q1.BO.Abstract;

public interface ISilverJewelryService
{
    Task CreateSJ(Request.Create request);
    Task UpdateSJ(Request.Create request);
    Task DeleteSJ(string id);
    Task<List<Request.Create>> GetAll(string? searchTerm);
    Task<Request.Create> GetById(string id);
}