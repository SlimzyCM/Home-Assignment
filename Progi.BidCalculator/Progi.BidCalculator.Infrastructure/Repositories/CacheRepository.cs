using Progi.BidCalculator.Core.Interfaces.DataAccess;
using Progi.BidCalculator.Infrastructure.Caching;
using System.Linq.Expressions;

namespace Progi.BidCalculator.Infrastructure.Repositories;

public sealed class CachedRepository<T>(IRepository<T> asyncRepository, ICacheManager cacheManager) : ICachedRepository<T>
    where T : class
{
    private readonly string _entityName = typeof(T).Name;

    public async Task<T?> GetByIdAsync(long id)
    {
        var cacheKey = $"{_entityName}_GetById_{id}";

        return await cacheManager.GetOrCreateAsync(cacheKey, async () => await asyncRepository.GetByIdAsync(id));
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var cacheKey = $"{_entityName}_GetAll";

        return await cacheManager.GetOrCreateAsync(cacheKey, asyncRepository.GetAllAsync);
    }

    public IQueryable<T> GetByExpressionAsync(Expression<Func<T, bool>> expression) => asyncRepository.GetByExpressionAsync(expression);

    public async Task<T> AddAsync(T entity)
    {
        var result = await asyncRepository.AddAsync(entity);
        var cacheKey = $"{_entityName}_GetById_{entity.GetType().GetProperty("Id")?.GetValue(entity)}";
        cacheManager.Remove(cacheKey);
        return result;
    }

    public async Task UpdateAsync(T entity)
    {
        await asyncRepository.UpdateAsync(entity);
        var cacheKey = $"{_entityName}_GetById_{entity.GetType().GetProperty("Id")?.GetValue(entity)}";
        cacheManager.Remove(cacheKey);
    }

    public async Task DeleteAsync(T entity)
    {
        await asyncRepository.DeleteAsync(entity);
        var cacheKey = $"{_entityName}_GetById_{entity.GetType().GetProperty("Id")?.GetValue(entity)}";
        cacheManager.Remove(cacheKey);
    }

    public IQueryable<T> GetAll() => asyncRepository.GetAll();
}