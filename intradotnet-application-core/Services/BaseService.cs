using IntraDotNet.Application.Core.Interfaces;
using IntraDotNet.Application.Core.Results;

namespace IntraDotNet.Application.Core.Services;

/// <summary>
/// Defines the base service interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <remarks>
/// This interface provides methods for creating, updating, deleting, and retrieving entities.
/// It is designed to be implemented by services that manage entities in a data store.
/// </remarks>
public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    /// <summary>
    /// Creates a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the creation operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the creation logic for the entity.
    /// It should return a ValueResult containing the created entity or an error if the creation fails.
    /// </remarks>
    public abstract Task<ValueResult<TEntity>> CreateAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the update operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the update logic for the entity.
    /// It should return a ValueResult containing the updated entity or an error if the update fails.
    /// </remarks>
    public abstract Task<ValueResult<TEntity>> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the deletion operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the deletion logic for the entity.
    /// It should return a ValueResult indicating success or failure of the deletion operation.
    /// </remarks>
    public abstract Task<ValueResult<TEntity>> DeleteAsync(TEntity entity);

    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of all entities.</returns>
    /// <remarks>    
    /// This method should be implemented to retrieve all entities from the data store.
    /// It should return a ValueResult containing an enumerable collection of entities or an error if the retrieval fails.
    /// </remarks>
    public abstract Task<ValueResult<IEnumerable<TEntity>>> GetAllAsync();

    /// <summary>
    /// Finds entities that match a specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">A function that defines the criteria to match entities.</param>
    /// <returns>A task that represents the asynchronous operation, containing a collection of entities that match the predicate.</returns>
    /// <remarks>
    /// This method should be implemented to filter entities based on the provided predicate.
    /// It should return a ValueResult containing an enumerable collection of entities that match the predicate or an error if the operation fails.
    /// </remarks>
    public abstract Task<ValueResult<IEnumerable<TEntity>>> FindAsync(Func<TEntity, bool> predicate);
}