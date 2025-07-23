using IntraDotNet.Application.Core.Results;

namespace IntraDotNet.Application.Core.Interfaces;

/// <summary>
/// Defines the base service interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <remarks>
/// This interface provides methods for creating, updating, deleting, and retrieving entities.
/// It is designed to be implemented by services that manage entities in a data store.
/// </remarks>
public interface IBaseService<TEntity> where TEntity : class
{
    Task<ValueResult<TEntity>> CreateAsync(TEntity entity);
    Task<ValueResult<TEntity>> DeleteAsync(TEntity entity);
    Task<ValueResult<IEnumerable<TEntity>>> FindAsync(Func<TEntity, bool> predicate);
    Task<ValueResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<ValueResult<TEntity>> UpdateAsync(TEntity entity);
}
