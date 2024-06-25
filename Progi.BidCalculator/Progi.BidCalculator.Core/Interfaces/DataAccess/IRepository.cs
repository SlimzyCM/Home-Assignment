using System.Linq.Expressions;

namespace Progi.BidCalculator.Core.Interfaces.DataAccess;

/// <summary>
/// Represents a generic repository interface for data access operations.
/// </summary>
/// <typeparam name="T">The type of entity managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<T?> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously retrieves all entities of the specified type.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Retrieves all entities of the specified type as a queryable source.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing the queryable source of entities.</returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Asynchronously retrieves entities that match the specified expression.
    /// </summary>
    /// <param name="expression">The expression to filter entities.</param>
    /// <returns>An <see cref="IQueryable{T}"/> representing the filtered queryable source of entities.</returns>
    IQueryable<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Asynchronously updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Asynchronously deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(T entity);
}