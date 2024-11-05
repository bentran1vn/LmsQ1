using Microsoft.EntityFrameworkCore;
using Q1.BO.Abstract;
using Q1.DAO.Abstract;
using Q1.DAO.Models;

namespace Q1.BO.Services.SilverJewelry;

public class SilverJewelryService : ISilverJewelryService
{
    private readonly IRepositoryBase<DAO.Models.SilverJewelry> _silverJewelryServiceRepository;
    private readonly IRepositoryBase<DAO.Models.Category> _categoryRepository;

    public SilverJewelryService(IRepositoryBase<DAO.Models.SilverJewelry> silverJewelryServiceRepository, IRepositoryBase<DAO.Models.Category> categoryRepository)
    {
        _silverJewelryServiceRepository = silverJewelryServiceRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task CreateSJ(Request.Create request)
    {
        var sJE = await _silverJewelryServiceRepository.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(request.SilverJewelryId));

        if (sJE is not null) throw new Exception("Siv Je Exist !");
        
        var cate = await _categoryRepository.FindSingleAsync(x =>
            x.CategoryId.Equals(request.CategoryId));

        if (cate is null) throw new Exception("cate is not Exist !");

        var sv = new DAO.Models.SilverJewelry()
        {
            SilverJewelryId = request.SilverJewelryId,
            CategoryId = request.CategoryId,
            Price = request.Price,
            MetalWeight = request.MetalWeight,
            SilverJewelryDescription = request.SilverJewelryDescription,
            ProductionYear = request.ProductionYear,
            SilverJewelryName = request.SilverJewelryName,
            CreatedDate = request.CreatedDate
        };
        
        _silverJewelryServiceRepository.Add(sv);
        await _silverJewelryServiceRepository.SaveChangesAsync();
    }

    public async Task DeleteSJ(string id)
    {
        var sJE = await _silverJewelryServiceRepository.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(id));

        if (sJE is null) throw new Exception("Siv Je is not Exist !");
        
        _silverJewelryServiceRepository.Remove(sJE);
        await _silverJewelryServiceRepository.SaveChangesAsync();
    }

    public async Task<List<Request.Create>> GetAll(string? searchTerm)
    {
        var query = string.IsNullOrWhiteSpace(searchTerm)
            ? _silverJewelryServiceRepository.FindAll(null, x => x.Category)
            : _silverJewelryServiceRepository.FindAll(
                x => x.SilverJewelryName.ToLower().Contains(searchTerm.ToLower()) || 
                     x.MetalWeight.ToString().Contains(searchTerm)
                , x => x.Category);

        var list = await query.ToListAsync();

        var result = list.Select(x => new Request.Create()
        {
            SilverJewelryId = x.SilverJewelryId,
            CategoryId = x.Category.CategoryId,
            Price = x.Price ?? 0,
            MetalWeight = x.MetalWeight ?? 0,
            CreatedDate = x.CreatedDate ?? DateTime.Now,
            ProductionYear = x.ProductionYear ?? 0,
            SilverJewelryName = x.SilverJewelryName,
            SilverJewelryDescription = x.SilverJewelryDescription
        }).ToList();

        return result;
    }

    public async Task UpdateSJ(Request.Create request)
    {
        var sJE = await _silverJewelryServiceRepository.FindSingleAsync(x =>
            x.SilverJewelryId.Equals(request.SilverJewelryId));

        if (sJE is null) throw new Exception("Siv Je is not Exist !");
        
        var cate = await _categoryRepository.FindSingleAsync(x =>
            x.CategoryId.Equals(request.CategoryId));

        if (cate is null) throw new Exception("cate is not Exist !");

        // var sv = new DAO.Models.SilverJewelry()
        // {
        //     SilverJewelryId = request.SilverJewelryId,
        //     CategoryId = request.CategoryId,
        //     Price = request.Price,
        //     MetalWeight = request.MetalWeight,
        //     SilverJewelryDescription = request.SilverJewelryDescription,
        //     ProductionYear = request.ProductionYear,
        //     SilverJewelryName = request.SilverJewelryName,
        //     CreatedDate = request.CreatedDate
        // };
        //
        // _silverJewelryServiceRepository.Update(sv);

        sJE.CategoryId = request.CategoryId;
        sJE.Price = request.Price;
        sJE.MetalWeight = request.MetalWeight;
        sJE.SilverJewelryDescription = request.SilverJewelryDescription;
        sJE.ProductionYear = request.ProductionYear;
        sJE.SilverJewelryName = request.SilverJewelryName;
        sJE.CreatedDate = request.CreatedDate;
        
        await _silverJewelryServiceRepository.SaveChangesAsync();
    }
}