using System.Linq.Expressions;

namespace Q1.DAO.Abstract;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[]? includeProperties);

    void Add(TEntity entity);
    
    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
    
    void UpdateRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveMultiple(List<TEntity> entities);
    
    Task SaveChangesAsync();
}