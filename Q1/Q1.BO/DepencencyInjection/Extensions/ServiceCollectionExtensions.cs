using Microsoft.Extensions.DependencyInjection;
using Q1.BO.Abstract;
using Q1.BO.Extensions;
using Q1.BO.Services.Identity;
using Q1.BO.Services.SilverJewelry;
using Q1.BO.Services.Category;

namespace Q1.BO.DepencencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServicesBo(this IServiceCollection services)
        => services
            .AddTransient<IJwtTokenService, JwtTokenService>()
            .AddTransient<IIdentityServices, IdentityService>()
            .AddTransient<ISilverJewelryService, SilverJewelryService>()
            .AddTransient<ICategoryService, CategoryService>();
}