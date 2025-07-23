namespace IntraDotNet.Application.Core.Interfaces;

/// <summary>
/// Defines the contract for a Unit of Work pattern implementation.
/// Manages transactions and coordinates changes across multiple repositories.
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask<TTransaction> BeginTransactionAsync<TTransaction>(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <returns>The database transaction.</returns>
    TTransaction BeginTransaction<TTransaction>();

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    int SaveChanges();

    /// <summary>
    /// Resets the unit of work by disposing and recreating the context.
    /// </summary>
    ValueTask ResetAsync();

    /// <summary>
    /// Resets the unit of work by disposing and recreating the context.
    /// </summary>
    void Reset();
}