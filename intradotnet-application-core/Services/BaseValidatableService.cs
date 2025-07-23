using IntraDotNet.Application.Core.Interfaces;
using IntraDotNet.Application.Core.Results;

namespace IntraDotNet.Application.Core.Services;

/// <summary>
/// Defines the base service interface for entities that require validation.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <remarks>
/// This interface extends the base service interface to include a method for validating entities.
/// It is designed to be implemented by services that manage entities with validation requirements.
/// </remarks>
public abstract class BaseValidatableService<TEntity> : BaseService<TEntity>, IBaseValidatableService<TEntity> where TEntity : class
{
    /// <summary>
    /// Creates a new entity asynchronously with validation.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the creation operation.</returns>
    /// <remarks>
    /// This method first validates the entity using the <see cref="ValidateAsync"/> method.
    /// If validation fails, it returns a ValueResult containing the error(s).
    /// If validation passes, it proceeds with the creation logic defined in <see cref="CreateInternalAsync"/>.
    /// </remarks>
    public override async Task<ValueResult<TEntity>> CreateAsync(TEntity entity)
    {
        // 1. Validate first (with business context)
        ValueResult<bool> validationResult = await ValidateAsync(entity);

        if (validationResult.IsFailure)
        {
            // 2. If validation fails, return the error(s)
            return ValueResult<TEntity>.Failure(validationResult.AggregateErrors!);
        }

        // 3. If validation passes, proceed with creation
        return await CreateInternalAsync(entity);
    }

    /// <summary>
    /// Creates a new entity internally.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the creation operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the actual creation logic for the entity.
    /// It should return a ValueResult containing the created entity or an error if the creation fails.
    /// </remarks>
    protected abstract Task<ValueResult<TEntity>> CreateInternalAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity asynchronously with validation/>.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the update operation.</returns>
    /// <remarks>    
    /// This method first validates the entity using the <see cref="ValidateAsync"/> method.
    /// If validation fails, it returns a ValueResult containing the error(s).
    /// If validation passes, it proceeds with the update logic defined in <see cref="UpdateInternalAsync"/>.
    /// </remarks>
    public override async Task<ValueResult<TEntity>> UpdateAsync(TEntity entity)
    {
        // 1. Validate first (with business context)
        ValueResult<bool> validationResult = await ValidateAsync(entity);

        if (validationResult.IsFailure)
        {
            // 2. If validation fails, return the error(s)
            return ValueResult<TEntity>.Failure(validationResult.AggregateErrors!);
        }

        // 3. If validation passes, proceed with update
        return await UpdateInternalAsync(entity);
    }

    /// <summary>
    /// Updates an existing entity internally.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the update operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the actual update logic for the entity.
    /// It should return a ValueResult containing the updated entity or an error if the update fails.
    /// </remarks>
    protected abstract Task<ValueResult<TEntity>> UpdateInternalAsync(TEntity entity);

    /// <summary>
    /// Validates an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the validation operation.</returns>
    /// <remarks>
    /// This method should be implemented to handle the validation logic for the entity.
    /// It should return a ValueResult indicating whether the entity is valid or not, along with any validation errors.
    /// </remarks>
    public abstract Task<ValueResult<bool>> ValidateAsync(TEntity entity);
}