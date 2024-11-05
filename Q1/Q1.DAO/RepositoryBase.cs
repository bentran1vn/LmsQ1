using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Q1.DAO.Abstract;

namespace Q1.DAO;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable
    where TEntity : class
{
    private readonly SilverJewelry2023DbContext _dbContext;

    public RepositoryBase(SilverJewelry2023DbContext dbContext)
        => _dbContext = dbContext;

    public void Dispose()
        => _dbContext?.Dispose();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[]? includeProperties)
    {
        IQueryable<TEntity> items = _dbContext.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties)
            .AsTracking()
            .SingleOrDefaultAsync(predicate, cancellationToken);

    public void Add(TEntity entity)
        => _dbContext.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities)
        => _dbContext.AddRange(entities);

    public void UpdateRange(IEnumerable<TEntity> entities)
        => _dbContext.UpdateRange(entities);

    public void Remove(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

    public void RemoveMultiple(List<TEntity> entities)
        => _dbContext.Set<TEntity>().RemoveRange(entities);

    public Task SaveChangesAsync()
        => _dbContext.SaveChangesAsync();

    public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
}