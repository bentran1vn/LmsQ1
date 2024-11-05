using Microsoft.Extensions.DependencyInjection;
using Q1.DAO.Abstract;

namespace Q1.DAO.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositoryPersistence(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}