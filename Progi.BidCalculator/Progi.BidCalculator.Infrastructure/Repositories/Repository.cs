using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Core.Interfaces.DataAccess;
using System.Linq.Expressions;

namespace Progi.BidCalculator.Infrastructure.Repositories;

public class Repository<T>(CalculatorContext dbContext) : IRepository<T> where T : class
{
    protected readonly CalculatorContext DbContext = dbContext ?? throw new ArgumentNullException();

    public async Task<T?> GetByIdAsync(long id) => await DbContext.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await DbContext.Set<T>().ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public IQueryable<T> GetByExpressionAsync(Expression<Func<T, bool>> expression) => DbContext.Set<T>().Where(expression);

    public IQueryable<T> GetAll() => DbContext.Set<T>().AsNoTracking();
}